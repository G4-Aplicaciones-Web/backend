using AlimentateplusPlatform.API.Shared.Domain.Repositories;
using AlimentateplusPlatform.API.Tracking.Domain.Model.ValueObjects;

namespace AlimentateplusPlatform.API.Tracking.Domain.Repositories;

public interface ITrackingRepository : IBaseRepository<Model.Aggregates.Tracking>
{
    Task<Model.Aggregates.Tracking?> FindByUserIdAsync(UserId userId);
    Task<bool> ExistsByUserIdAsync(UserId userId);
}