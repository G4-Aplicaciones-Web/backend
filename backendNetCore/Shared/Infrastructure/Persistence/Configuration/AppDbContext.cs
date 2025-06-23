using backendNetCore.MealPlans.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.Entities;
using backendNetCore.Recommendations.Domain.Model.Aggregates;
using backendNetCore.Recommendations.Domain.Model.Entities;
using backendNetCore.Shared.Infrastructure.Persistence.Configuration.Extensions;
using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Model.ValueObjects;
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

        // =================== RECIPES CONTEXT ==================
        
        
        // Recipes Context
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



        
        // Meal Plans Context
        modelBuilder.Entity<MealPlan>().HasKey(m => m.Id);
        modelBuilder.Entity<MealPlan>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<MealPlan>().Property(m => m.ProfileId).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<MealPlan>().Property(m => m.Summary).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<MealPlan>().Property(m => m.Score).IsRequired().HasDefaultValue(10);
        
        // =================== TRACKING CONTEXT ==================
        
         // Tracking - Configuración principal
         modelBuilder.Entity<Tracking.Domain.Model.Aggregates.Tracking>()
            .HasKey(t => t.Id);

         modelBuilder.Entity<Tracking.Domain.Model.Aggregates.Tracking>()
            .Property(t => t.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

         modelBuilder.Entity<Tracking.Domain.Model.Aggregates.Tracking>()
            .Property(t => t.Date)
            .HasColumnName("tracking_date")
            .IsRequired();

         modelBuilder.Entity<Tracking.Domain.Model.Aggregates.Tracking>()
            .Property(t => t.UserProfileId)
            .HasConversion(
                v => v.Id,
                v => new UserId(v))
            .HasColumnName("user_profile_id");

        // MealPlanEntry - Configuración como entidad independiente
        modelBuilder.Entity<MealPlanEntry>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<MealPlanEntry>()
            .Property(e => e.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<MealPlanEntry>()
            .Property(e => e.DayNumber)
            .HasColumnName("day_number")
            .IsRequired();

        // Configurar TrackingId con el nombre de columna correcto
        modelBuilder.Entity<MealPlanEntry>()
            .Property(e => e.TrackingId)
            .HasColumnName("tracking_id")
            .IsRequired();

        modelBuilder.Entity<MealPlanEntry>()
            .Property(e => e.RecipeId)
            .HasConversion(
                v => v.Id,
                v => new RecipeId(v))
            .HasColumnName("recipe_id");

        // Configurar la relación MealPlanEntry -> Tracking (SIN HasColumnName)
        modelBuilder.Entity<MealPlanEntry>()
            .HasOne<Tracking.Domain.Model.Aggregates.Tracking>()
            .WithMany()
            .HasForeignKey(e => e.TrackingId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configurar la relación con MealPlanType (entidad independiente)
        modelBuilder.Entity<MealPlanEntry>()
            .HasOne(e => e.MealPlanType)
            .WithMany()
            .HasForeignKey("MealPlanTypeId")
            .IsRequired();

        // Configurar el nombre de la columna para la foreign key shadow property
        modelBuilder.Entity<MealPlanEntry>()
            .Property<long>("MealPlanTypeId")
            .HasColumnName("meal_plan_type_id");

        // Ignorar la propiedad calculada MealPlanEntryList
        modelBuilder.Entity<Tracking.Domain.Model.Aggregates.Tracking>()
            .Ignore(t => t.MealPlanEntryList);

        // Configurar la relación Tracking -> MealPlanEntry (uno a muchos)
        modelBuilder.Entity<Tracking.Domain.Model.Aggregates.Tracking>()
            .HasMany(t => t.MealPlanEntriesInternal)
            .WithOne(e => e.Tracking) // ← esta parte es CLAVE
            .HasForeignKey(e => e.TrackingId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Tracking.Domain.Model.Aggregates.Tracking>()
            .HasOne(t => t.TrackingGoal)
            .WithMany()
            .HasForeignKey("TrackingGoalId")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Tracking.Domain.Model.Aggregates.Tracking>()
            .HasOne(t => t.TrackingMacronutrient)
            .WithOne()
            .HasForeignKey<Tracking.Domain.Model.Aggregates.Tracking>("TrackingMacronutrientId")
            .OnDelete(DeleteBehavior.Cascade);

        // MealPlanType - Solo si es una entidad independiente
        modelBuilder.Entity<MealPlanType>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<MealPlanType>()
            .Property(m => m.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<MealPlanType>()
            .Property(m => m.Name)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired()
            .HasColumnName("name");
        
        // Seed Data de MealPlanType
        modelBuilder.Entity<MealPlanType>().HasData(
            new MealPlanType { Id = 1, Name = (MealPlanTypes)Enum.Parse(typeof(MealPlanTypes), "Breakfast") },
            new MealPlanType { Id = 2, Name = (MealPlanTypes)Enum.Parse(typeof(MealPlanTypes), "Lunch") },
            new MealPlanType { Id = 3, Name = (MealPlanTypes)Enum.Parse(typeof(MealPlanTypes), "Dinner") },
            new MealPlanType { Id = 4, Name = (MealPlanTypes)Enum.Parse(typeof(MealPlanTypes), "Healthy") }
        );

        // TrackingGoal
        modelBuilder.Entity<TrackingGoal>()
            .HasKey(g => g.Id);

        modelBuilder.Entity<TrackingGoal>()
            .Property(g => g.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<TrackingGoal>()
            .Property(g => g.UserId)
            .HasConversion(
                v => v.Id,
                v => new UserId(v))
            .HasColumnName("user_id");

        modelBuilder.Entity<TrackingGoal>()
            .HasOne(g => g.TargetMacros)
            .WithMany()
            .HasForeignKey("TargetMacrosId");

        // TrackingMacronutrient
        modelBuilder.Entity<TrackingMacronutrient>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<TrackingMacronutrient>()
            .Property(m => m.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<TrackingMacronutrient>().Property(m => m.Calories).IsRequired();
        modelBuilder.Entity<TrackingMacronutrient>().Property(m => m.Carbs).IsRequired();
        modelBuilder.Entity<TrackingMacronutrient>().Property(m => m.Proteins).IsRequired();
        modelBuilder.Entity<TrackingMacronutrient>().Property(m => m.Fats).IsRequired();
        
        modelBuilder.UseSnakeCaseNamingConvention();
    }
    
    public DbSet<Tracking.Domain.Model.Aggregates.Tracking> Trackings => Set<Tracking.Domain.Model.Aggregates.Tracking>();
    public DbSet<TrackingGoal> TrackingGoals => Set<TrackingGoal>();
    public DbSet<TrackingMacronutrient> TrackingMacronutrients => Set<TrackingMacronutrient>();
    public DbSet<MealPlanEntry> MealPlanEntries => Set<MealPlanEntry>();
    public DbSet<MealPlanType> MealPlanTypes => Set<MealPlanType>();
}
