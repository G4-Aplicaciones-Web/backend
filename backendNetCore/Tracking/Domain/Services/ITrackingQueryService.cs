using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Model.Queries;

namespace backendNetCore.Tracking.Domain.Services;

/// <summary>
/// Tracking query service interface
/// </summary>
public interface ITrackingQueryService
{
    Task<List<MealPlanEntry>> Handle(GetAllMealsQuery query);
    Task<Model.Aggregates.Tracking?> Handle(GetTrackingByUserIdQuery query);
    Task<TrackingMacronutrient?> Handle(GetConsumedMacrosQuery query);
}