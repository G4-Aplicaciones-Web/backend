using AlimentateplusPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using AlimentateplusPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;
using AlimentateplusPlatform.API.Tracking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AlimentateplusPlatform.API.Tracking.Infrastructure.Persistence.EFC.Repositories;

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