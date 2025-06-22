using AlimentateplusPlatform.API.Shared.Domain.Repositories;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;
using AlimentateplusPlatform.API.Tracking.Domain.Model.ValueObjects;

namespace AlimentateplusPlatform.API.Tracking.Domain.Repositories;

public interface ITrackingGoalRepository : IBaseRepository<TrackingGoal>
{
    Task<TrackingGoal?> FindByUserIdAsync(UserId userId);
    Task<bool> ExistsByUserIdAsync(UserId userId);

    new Task<TrackingGoal?> FindByIdAsync(int id);
}