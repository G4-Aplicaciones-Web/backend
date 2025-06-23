using backendNetCore.Shared.Domain.Repositories;
using backendNetCore.Tracking.Domain.Model.Entities;

namespace backendNetCore.Tracking.Domain.Repositories;

public interface IMealPlanEntryRepository : IBaseRepository<MealPlanEntry>
{
    Task<List<MealPlanEntry>> FindAllByTrackingIdAsync(long trackingId);
}