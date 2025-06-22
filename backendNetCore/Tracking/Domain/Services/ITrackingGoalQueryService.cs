using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Queries;

namespace AlimentateplusPlatform.API.Tracking.Domain.Services;

public interface ITrackingGoalQueryService
{
    Task<TrackingGoal?> Handle(GetTrackingGoalByUserIdQuery query);
    Task<TrackingMacronutrient?> Handle(GetTargetMacronutrientsQuery query);
    
}