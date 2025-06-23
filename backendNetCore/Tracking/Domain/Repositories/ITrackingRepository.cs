using backendNetCore.Shared.Domain.Repositories;
using backendNetCore.Tracking.Domain.Model.ValueObjects;

namespace backendNetCore.Tracking.Domain.Repositories;

public interface ITrackingRepository : IBaseRepository<Model.Aggregates.Tracking>
{
    Task<Model.Aggregates.Tracking?> FindByUserIdAsync(UserId userId);
    Task<bool> ExistsByUserIdAsync(UserId userId);
}