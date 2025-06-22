using backendNetCore.MealPlans.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.Entities;
using backendNetCore.Recipes.Domain.Model.ValueObjects;
using backendNetCore.Shared.Infrastructure.Persistence.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace backendNetCore.Shared.Infrastructure.Persistence.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        
        // Recipes Context
        modelBuilder.Entity<Recipe>().HasKey(r => r.Id);
        modelBuilder.Entity<Recipe>().Property(r=>r.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Recipe>().Property(r=>r.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Recipe>().Property(r=>r.Description).IsRequired().HasMaxLength(500);
        modelBuilder.Entity<Recipe>().Property(r=>r.UrlInstructions).HasMaxLength(500);
        
        // Recipe Type Enum Configuration
        modelBuilder.Entity<Recipe>()
            .Property(r => r.RecipeType)
            .HasConversion<string>()
            .IsRequired();

        // UserId Value Object Configuration
        modelBuilder.Entity<Recipe>()
            .Property(r => r.UserId)
            .HasConversion(
                v => v.Id,
                v => new UserId(v))
            .IsRequired();

        // MacronutrientValues Value Object Configuration (Owned Entity)
        modelBuilder.Entity<Recipe>()
            .OwnsOne(r => r.TotalNutrients, nutrients =>
            {
                nutrients.Property(n => n.Calories).HasColumnName("total_calories").HasPrecision(8, 2);
                nutrients.Property(n => n.Proteins).HasColumnName("total_proteins").HasPrecision(8, 2);
                nutrients.Property(n => n.Carbohydrates).HasColumnName("total_carbohydrates").HasPrecision(8, 2);
                nutrients.Property(n => n.Fats).HasColumnName("total_fats").HasPrecision(8, 2);
                
                nutrients.WithOwner().HasForeignKey("Id");
                nutrients.Property<int>("Id").HasColumnName("id");
            });
        
        // Ignore Ingredients public collection to avoid mapping issues
        modelBuilder.Entity<Recipe>()
            .Ignore(r => r.Ingredients);
        // Relationship: Recipe -> IngredientQuantity (One-to-Many)
        modelBuilder.Entity<Recipe>()
            .HasMany<IngredientQuantity>("_ingredients")
            .WithOne()
            .HasForeignKey("RecipeId")
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Ingredient>().HasKey(i => i.Id);
        modelBuilder.Entity<Ingredient>().Property(i=>i.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Ingredient>().Property(i=>i.Name).IsRequired().HasMaxLength(100);
        
        // Ingredient Category Enum Configuration
        modelBuilder.Entity<Ingredient>()
            .Property(i => i.IngredientCategory)
            .HasConversion<string>()
            .IsRequired();

        // Ingredient MacronutrientValues Configuration (Owned Entity)
        modelBuilder.Entity<Ingredient>()
            .OwnsOne(i => i.Nutrients, nutrients =>
            {
                nutrients.Property(n => n.Calories).HasColumnName("calories").HasPrecision(8, 2);
                nutrients.Property(n => n.Proteins).HasColumnName("proteins").HasPrecision(8, 2);
                nutrients.Property(n => n.Carbohydrates).HasColumnName("carbohydrates").HasPrecision(8, 2);
                nutrients.Property(n => n.Fats).HasColumnName("fats").HasPrecision(8, 2);
                
                nutrients.WithOwner().HasForeignKey("Id");
                nutrients.Property<int>("Id").HasColumnName("id"); // Explicitly map the shadow PK property for the owned type
            });

        // IngredientQuantity Entity Configuration
        modelBuilder.Entity<IngredientQuantity>().HasKey(iq => iq.Id);
        modelBuilder.Entity<IngredientQuantity>().Property(iq => iq.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<IngredientQuantity>().Property(iq => iq.IngredientId).IsRequired();
        modelBuilder.Entity<IngredientQuantity>().Property(iq => iq.Quantity).IsRequired().HasPrecision(8, 2);

        // Relationship: IngredientQuantity -> Ingredient (Many-to-One)
        modelBuilder.Entity<IngredientQuantity>()
            .HasOne<Ingredient>()
            .WithMany()
            .HasForeignKey(iq => iq.IngredientId)
            .OnDelete(DeleteBehavior.Restrict);

        // Shadow Property for RecipeId in IngredientQuantity
        modelBuilder.Entity<IngredientQuantity>()
            .Property<int>("RecipeId")
            .IsRequired();
        
        // Meal Plans Context
        modelBuilder.Entity<MealPlan>().HasKey(m => m.Id);
        modelBuilder.Entity<MealPlan>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<MealPlan>().Property(m => m.ProfileId).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<MealPlan>().Property(m => m.Summary).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<MealPlan>().Property(m => m.Score).IsRequired().HasDefaultValue(10);
        
        modelBuilder.UseSnakeCaseNamingConvention();
    }
}