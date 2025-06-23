using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Model.Queries;

namespace backendNetCore.Tracking.Domain.Services;

public interface ITrackingGoalQueryService
{
    Task<TrackingGoal?> Handle(GetTrackingGoalByUserIdQuery query);
    Task<TrackingMacronutrient?> Handle(GetTargetMacronutrientsQuery query);
    
}