using backendNetCore.Shared.Infrastructure.Persistence.Configuration;
using backendNetCore.Shared.Infrastructure.Persistence.Repositories;
using backendNetCore.Tracking.Domain.Model.ValueObjects;
using backendNetCore.Tracking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backendNetCore.Tracking.Infrastructure.Persistence.EFC.Repositories;

public class TrackingRepository(AppDbContext context) : BaseRepository<Domain.Model.Aggregates.Tracking>(context), ITrackingRepository
{
    public async Task<Domain.Model.Aggregates.Tracking> FindByUserIdAsync(UserId userId) =>
        await Context.Set<Domain.Model.Aggregates.Tracking>()
            .Include(t => t.MealPlanEntriesInternal)
            .Include(t => t.TrackingGoal)
            .Where(t => t.UserProfileId == userId)
            .FirstOrDefaultAsync();

    public async Task<bool> ExistsByUserIdAsync(UserId userId) =>
        await Context.Set<Domain.Model.Aggregates.Tracking>()
            .AnyAsync(t => t.UserProfileId == userId);

    // Override para incluir entidades relacionadas en FindByIdAsync
    public new async Task<Domain.Model.Aggregates.Tracking?> FindByIdAsync(long id) =>
        await Context.Set<Domain.Model.Aggregates.Tracking>()
            .Include(t => t.MealPlanEntriesInternal)
            .Include(t => t.TrackingGoal)
            .Where(t => t.Id == id)
            .FirstOrDefaultAsync();

    // Override para incluir entidades relacionadas en ListAsync
    public new async Task<IEnumerable<Domain.Model.Aggregates.Tracking>> ListAsync() =>
        await Context.Set<Domain.Model.Aggregates.Tracking>()
            .Include(t => t.MealPlanEntriesInternal)
            .Include(t => t.TrackingGoal)
            .ToListAsync();
}