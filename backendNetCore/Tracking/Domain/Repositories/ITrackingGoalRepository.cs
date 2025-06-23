using backendNetCore.Shared.Domain.Repositories;
using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Model.ValueObjects;

namespace backendNetCore.Tracking.Domain.Repositories;

public interface ITrackingGoalRepository : IBaseRepository<TrackingGoal>
{
    Task<TrackingGoal?> FindByUserIdAsync(UserId userId);
    Task<bool> ExistsByUserIdAsync(UserId userId);

    new Task<TrackingGoal?> FindByIdAsync(int id);
}