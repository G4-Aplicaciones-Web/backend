using backendNetCore.Shared.Infrastructure.Persistence.Configuration;
using backendNetCore.Shared.Infrastructure.Persistence.Repositories;
using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backendNetCore.Tracking.Infrastructure.Persistence.EFC.Repositories;

public class MealPlanEntryRepository(AppDbContext context)
    : BaseRepository<MealPlanEntry>(context), IMealPlanEntryRepository
{
    public async Task<List<MealPlanEntry>> FindAllByTrackingIdAsync(long trackingId) =>
        await Context.Set<MealPlanEntry>()
            .Include(e => e.MealPlanType) // <--- Esto carga la relación
            .Where(e => e.TrackingId == trackingId)
            .ToListAsync();
}