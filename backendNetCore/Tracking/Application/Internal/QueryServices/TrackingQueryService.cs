using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Queries;
using AlimentateplusPlatform.API.Tracking.Domain.Model.ValueObjects;
using AlimentateplusPlatform.API.Tracking.Domain.Repositories;
using AlimentateplusPlatform.API.Tracking.Domain.Services;

namespace AlimentateplusPlatform.API.Tracking.Application.Internal.QueryServices;

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