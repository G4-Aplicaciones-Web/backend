using AlimentateplusPlatform.API.Shared.Domain.Repositories;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;

namespace AlimentateplusPlatform.API.Tracking.Domain.Repositories;

public interface IMealPlanEntryRepository : IBaseRepository<MealPlanEntry>
{
    Task<List<MealPlanEntry>> FindAllByTrackingIdAsync(long trackingId);
}