using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Model.Queries;
using backendNetCore.Tracking.Domain.Model.ValueObjects;
using backendNetCore.Tracking.Domain.Repositories;
using backendNetCore.Tracking.Domain.Services;

namespace backendNetCore.Tracking.Application.Internal.QueryServices;

/// <summary>
/// Tracking query service
/// </summary>
/// <param name="trackingRepository">
/// Tracking repository
/// </param>
public class TrackingQueryService(
    ITrackingRepository trackingRepository,
    ITrackingMacronutrientRepository macrosRepository,
    IMealPlanEntryRepository entryRepository)
    : ITrackingQueryService
{
    public async Task<List<MealPlanEntry>> Handle(GetAllMealsQuery query)
        => await entryRepository.FindAllByTrackingIdAsync(query.TrackingId);

    public async Task<Domain.Model.Aggregates.Tracking?> Handle(GetTrackingByUserIdQuery query)
        => await trackingRepository.FindByUserIdAsync(query.UserId);

    public async Task<TrackingMacronutrient?> Handle(GetConsumedMacrosQuery query)
    {
        var tracking = await trackingRepository.FindByIdAsync(query.TrackingId);
        return tracking?.TrackingMacronutrient;
    }
}