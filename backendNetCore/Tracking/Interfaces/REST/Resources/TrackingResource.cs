using backendNetCore.Tracking.Interfaces.REST.Resources;

namespace AlimentateplusPlatform.API.Tracking.Interfaces.REST.Resources;

public record TrackingResource( long Id,
    long UserId,
    DateTime Date,
    TrackingMacronutrientResource ConsumedMacros,
    List<MealPlanEntriesResource> MealPlanEntries);