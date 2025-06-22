using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.Entities;
using backendNetCore.Recipes.Domain.Model.ValueObjects;
using backendNetCore.Recommendations.Domain.Model.Aggregates;
using backendNetCore.Recommendations.Domain.Model.Entities;
using backendNetCore.Shared.Infrastructure.Persistence.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

// Aliases para evitar ambigüedad entre UserId
using RecipesUserId = backendNetCore.Recipes.Domain.Model.ValueObjects.UserId;
using RecommendationsUserId = backendNetCore.Recommendations.Domain.Model.ValueObjects.UserId;

namespace backendNetCore.Shared.Infrastructure.Persistence.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Recommendation> Recommendations { get; set; } = null!;
    public DbSet<RecommendationTemplate> RecommendationTemplates { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =================== RECOMMENDATIONS CONTEXT ===================

        modelBuilder.Entity<RecommendationTemplate>().HasKey(rt => rt.Id);
        modelBuilder.Entity<RecommendationTemplate>().Property(rt => rt.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<RecommendationTemplate>().Property(rt => rt.Title).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<RecommendationTemplate>().Property(rt => rt.Content).IsRequired().HasColumnType("TEXT");

        modelBuilder.Entity<Recommendation>().HasKey(r => r.Id);
        modelBuilder.Entity<Recommendation>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Recommendation>().Property(r => r.Reason).IsRequired().HasMaxLength(200);
        modelBuilder.Entity<Recommendation>().Property(r => r.Notes).IsRequired().HasColumnType("TEXT");
        modelBuilder.Entity<Recommendation>().Property(r => r.Score).IsRequired().HasPrecision(5, 2);
        modelBuilder.Entity<Recommendation>().Property(r => r.AssignedAt).IsRequired();

        modelBuilder.Entity<Recommendation>()
            .Property(r => r.TimeOfDay)
            .HasConversion<string>()
            .IsRequired();

        modelBuilder.Entity<Recommendation>()
            .Property(r => r.Status)
            .HasConversion<string>()
            .IsRequired();

        // UserId (Recommendations)
        modelBuilder.Entity<Recommendation>()
            .Property(r => r.UserId)
            .HasConversion(
                uid => uid.Value,
                value => new RecommendationsUserId(value))
            .HasColumnName("user_id")
            .IsRequired();

        // Relationship: Recommendation → RecommendationTemplate
        modelBuilder.Entity<Recommendation>()
            .HasOne(r => r.Template)
            .WithMany(t => t.Recommendations)
            .HasForeignKey("TemplateId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        // =================== RECIPES CONTEXT ===================

        modelBuilder.Entity<Recipe>().HasKey(r => r.Id);
        modelBuilder.Entity<Recipe>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Recipe>().Property(r => r.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Recipe>().Property(r => r.Description).IsRequired().HasMaxLength(500);
        modelBuilder.Entity<Recipe>().Property(r => r.UrlInstructions).HasMaxLength(500);

        modelBuilder.Entity<Recipe>()
            .Property(r => r.RecipeType)
            .HasConversion<string>()
            .IsRequired();

        modelBuilder.Entity<Recipe>()
            .Property(r => r.UserId)
            .HasConversion(
                v => v.Id,
                v => new RecipesUserId(v))
            .IsRequired();

        modelBuilder.Entity<Recipe>().OwnsOne(r => r.TotalNutrients, nutrients =>
        {
            nutrients.Property(n => n.Calories).HasColumnName("total_calories").HasPrecision(8, 2);
            nutrients.Property(n => n.Proteins).HasColumnName("total_proteins").HasPrecision(8, 2);
            nutrients.Property(n => n.Carbohydrates).HasColumnName("total_carbohydrates").HasPrecision(8, 2);
            nutrients.Property(n => n.Fats).HasColumnName("total_fats").HasPrecision(8, 2);

            nutrients.WithOwner().HasForeignKey("Id");
            nutrients.Property<int>("Id").HasColumnName("id");
        });

        modelBuilder.Entity<Recipe>().Ignore(r => r.Ingredients);

        modelBuilder.Entity<Recipe>()
            .HasMany<IngredientQuantity>("_ingredients")
            .WithOne()
            .HasForeignKey("RecipeId")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Ingredient>().HasKey(i => i.Id);
        modelBuilder.Entity<Ingredient>().Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Ingredient>().Property(i => i.Name).IsRequired().HasMaxLength(100);

        modelBuilder.Entity<Ingredient>()
            .Property(i => i.IngredientCategory)
            .HasConversion<string>()
            .IsRequired();

        modelBuilder.Entity<Ingredient>().OwnsOne(i => i.Nutrients, nutrients =>
        {
            nutrients.Property(n => n.Calories).HasColumnName("calories").HasPrecision(8, 2);
            nutrients.Property(n => n.Proteins).HasColumnName("proteins").HasPrecision(8, 2);
            nutrients.Property(n => n.Carbohydrates).HasColumnName("carbohydrates").HasPrecision(8, 2);
            nutrients.Property(n => n.Fats).HasColumnName("fats").HasPrecision(8, 2);

            nutrients.WithOwner().HasForeignKey("Id");
            nutrients.Property<int>("Id").HasColumnName("id");
        });

        modelBuilder.Entity<IngredientQuantity>().HasKey(iq => iq.Id);
        modelBuilder.Entity<IngredientQuantity>().Property(iq => iq.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<IngredientQuantity>().Property(iq => iq.IngredientId).IsRequired();
        modelBuilder.Entity<IngredientQuantity>().Property(iq => iq.Quantity).IsRequired().HasPrecision(8, 2);

        modelBuilder.Entity<IngredientQuantity>()
            .HasOne<Ingredient>()
            .WithMany()
            .HasForeignKey(iq => iq.IngredientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<IngredientQuantity>()
            .Property<int>("RecipeId")
            .IsRequired();

        modelBuilder.UseSnakeCaseNamingConvention();
    }
}
