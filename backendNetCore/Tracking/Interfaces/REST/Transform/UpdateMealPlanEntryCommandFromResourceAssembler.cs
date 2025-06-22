using AlimentateplusPlatform.API.Tracking.Domain.Model.Commands;
using AlimentateplusPlatform.API.Tracking.Domain.Model.ValueObjects;
using AlimentateplusPlatform.API.Tracking.Interfaces.REST.Resources;

namespace AlimentateplusPlatform.API.Tracking.Interfaces.REST.Transform;

public class UpdateMealPlanEntryCommandFromResourceAssembler
{
    public static UpdateMealPlanEntryInTrackingCommand ToCommand(UpdateMealPlanEntryResource resource, int mealPlanEntryId)
    {
        return new UpdateMealPlanEntryInTrackingCommand(
            resource.TrackingId,
            mealPlanEntryId,
            new RecipeId(resource.RecipeId),
            Enum.Parse<MealPlanTypes>(resource.MealPlanType, true),
            resource.DayNumber
        );
    }
}