using backendNetCore.Shared.Infrastructure.Persistence.Configuration;
using backendNetCore.Shared.Infrastructure.Persistence.Repositories;
using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Model.ValueObjects;
using backendNetCore.Tracking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backendNetCore.Tracking.Infrastructure.Persistence.EFC.Repositories;

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