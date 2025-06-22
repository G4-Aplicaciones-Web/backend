using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Queries;
using AlimentateplusPlatform.API.Tracking.Domain.Repositories;
using AlimentateplusPlatform.API.Tracking.Domain.Services;

namespace AlimentateplusPlatform.API.Tracking.Application.Internal.QueryServices;

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