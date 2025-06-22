using AlimentateplusPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using AlimentateplusPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;
using AlimentateplusPlatform.API.Tracking.Domain.Model.ValueObjects;
using AlimentateplusPlatform.API.Tracking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AlimentateplusPlatform.API.Tracking.Infrastructure.Persistence.EFC.Repositories;

public class TrackingGoalRepository(AppDbContext context)
    : BaseRepository<TrackingGoal>(context), ITrackingGoalRepository
{
    public async Task<TrackingGoal?> FindByUserIdAsync(UserId userId) =>
        await Context.Set<TrackingGoal>()
            .Include(g => g.TargetMacros) // ← esto asegura que se cargue la relación
            .FirstOrDefaultAsync(g => g.UserId == userId);

    public async Task<bool> ExistsByUserIdAsync(UserId userId) =>
        await Context.Set<TrackingGoal>()
            .AnyAsync(g => g.UserId == userId);
    
    public new async Task<TrackingGoal?> FindByIdAsync(int id)
    {
        return await Context.Set<TrackingGoal>()
            .Include(g => g.TargetMacros)
            .FirstOrDefaultAsync(g => g.Id == id);
    }
    
}