using backendNetCore.Profiles.Domain.Model.Aggregates;
using backendNetCore.Profiles.Domain.Model.Entities;
using backendNetCore.Profiles.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace backendNetCore.Profiles.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyProfilesConfiguration(this ModelBuilder builder)
    {
        // Profile entity
        builder.Entity<Profile>().HasKey(p => p.Id);
        builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        
        builder.Entity<Profile>()
            .Property(p => p.UserId)
            .HasConversion(
                userId => userId.Value, // al guardar
                value => new UserId(value)) // al leer
            .HasColumnName("UserId")
            .IsRequired();

        builder.Entity<Profile>().OwnsOne(p => p.Name, n =>
        {
            n.WithOwner().HasForeignKey("Id");
            n.Property(p => p.FirstName).HasColumnName("FirstName");
            n.Property(p => p.LastName).HasColumnName("LastName");
        });

        builder.Entity<Profile>().Property(p => p.Gender).IsRequired().HasMaxLength(20);
        builder.Entity<Profile>().Property(p => p.Height).IsRequired();
        builder.Entity<Profile>().Property(p => p.Weight).IsRequired();
        builder.Entity<Profile>().Property(p => p.Score).IsRequired();

        // Allergies configuration (como coleccion privada)
        builder.Entity<Profile>().Ignore(p => p.Allergies);

        builder.Entity<Profile>()
            .HasMany(typeof(Allergy), "_allergies")
            .WithOne()
            .HasForeignKey("ProfileId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Allergy>().ToTable("profile_allergies");
        builder.Entity<Allergy>().HasKey(a => a.Id);
        builder.Entity<Allergy>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Allergy>().Property(a => a.Name).IsRequired().HasMaxLength(100).HasColumnName("allergy_name");

        // ActivityLevel entity
        builder.Entity<ActivityLevel>().HasKey(a => a.Id);
        builder.Entity<ActivityLevel>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ActivityLevel>().Property(a => a.Name).IsRequired().HasMaxLength(50);
        builder.Entity<ActivityLevel>().Property(a => a.Description).IsRequired().HasMaxLength(240);
        builder.Entity<ActivityLevel>().Property(a => a.ActivityFactor).IsRequired();

        // Objective entity
        builder.Entity<Objective>().HasKey(o => o.Id);
        builder.Entity<Objective>().Property(o => o.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Objective>().Property(o => o.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Objective>().Property(o => o.Score).IsRequired();
    }
}
