using backendNetCore.MealPlans.Domain.Model.Aggregates;
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

        modelBuilder.Entity<MealPlan>().HasKey(m => m.Id);
        modelBuilder.Entity<MealPlan>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<MealPlan>().Property(m => m.ProfileId).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<MealPlan>().Property(m => m.Summary).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<MealPlan>().Property(m => m.Score).IsRequired().HasDefaultValue(10);
        
        
        modelBuilder.UseSnakeCaseNamingConvention();
    }
}