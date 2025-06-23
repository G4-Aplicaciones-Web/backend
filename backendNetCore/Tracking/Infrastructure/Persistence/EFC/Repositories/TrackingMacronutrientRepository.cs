using backendNetCore.Shared.Infrastructure.Persistence.Configuration;
using backendNetCore.Shared.Infrastructure.Persistence.Repositories;
using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backendNetCore.Tracking.Infrastructure.Persistence.EFC.Repositories;

public class TrackingMacronutrientRepository(AppDbContext context)
    : BaseRepository<TrackingMacronutrient>(context), ITrackingMacronutrientRepository
{
    public async Task<bool> ExistsByValuesAsync(double calories, double carbs, double proteins, double fats) =>
        await Context.Set<TrackingMacronutrient>()
            .AnyAsync(m =>
                m.Calories == calories &&
                m.Carbs == carbs &&
                m.Proteins == proteins &&
                m.Fats == fats);
}