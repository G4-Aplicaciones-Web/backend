using AlimentateplusPlatform.API.Tracking.Domain.Model.Commands;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Queries;

namespace AlimentateplusPlatform.API.Tracking.Domain.Services;

/// <summary>
/// Tracking query service interface
/// </summary>
public interface ITrackingQueryService
{
    Task<List<MealPlanEntry>> Handle(GetAllMealsQuery query);
    Task<Model.Aggregates.Tracking?> Handle(GetTrackingByUserIdQuery query);
    Task<TrackingMacronutrient?> Handle(GetConsumedMacrosQuery query);
}