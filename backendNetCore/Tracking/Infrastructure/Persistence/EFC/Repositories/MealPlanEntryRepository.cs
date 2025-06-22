using AlimentateplusPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using AlimentateplusPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;
using AlimentateplusPlatform.API.Tracking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AlimentateplusPlatform.API.Tracking.Infrastructure.Persistence.EFC.Repositories;

public class MealPlanEntryRepository(AppDbContext context)
    : BaseRepository<MealPlanEntry>(context), IMealPlanEntryRepository
{
    public async Task<List<MealPlanEntry>> FindAllByTrackingIdAsync(long trackingId) =>
        await Context.Set<MealPlanEntry>()
            .Include(e => e.MealPlanType) // <--- Esto carga la relación
            .Where(e => e.TrackingId == trackingId)
            .ToListAsync();
}