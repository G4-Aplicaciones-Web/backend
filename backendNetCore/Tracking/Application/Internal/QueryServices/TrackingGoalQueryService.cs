using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Model.Queries;
using backendNetCore.Tracking.Domain.Repositories;
using backendNetCore.Tracking.Domain.Services;

namespace backendNetCore.Tracking.Application.Internal.QueryServices;

public class TrackingGoalQueryService(ITrackingGoalRepository repository)
    : ITrackingGoalQueryService
{
    public async Task<TrackingGoal?> Handle(GetTrackingGoalByUserIdQuery query)
        => await repository.FindByUserIdAsync(query.UserId);

    public async Task<TrackingMacronutrient?> Handle(GetTargetMacronutrientsQuery query)
    {
        var goal = await repository.FindByIdAsync(query.TrackingGoalId);
        return goal?.TargetMacros;
    }
}