using AlimentateplusPlatform.API.Tracking.Interfaces.REST.Resources;

namespace AlimentateplusPlatform.API.Tracking.Interfaces.REST.Transform;

using System.Linq;

using System.Collections.Generic;

public class TrackingResourceFromEntityAssembler
{
    public static TrackingResource ToResource(Domain.Model.Aggregates.Tracking tracking)
    {
        var consumed = TrackingMacronutrientResourceFromEntityAssembler.ToResource(tracking.TrackingMacronutrient);
        var entries = tracking.MealPlanEntryList.ConsumedMeals
            .Select(entry => MealPlanEntriesResourceFromEntityAssembler.ToResource(entry))
            .ToList();

        return new TrackingResource(
            tracking.Id,
            tracking.UserProfileId.Id,
            tracking.Date,
            consumed,
            entries
        );
    }
}