using backendNetCore.Tracking.Domain.Model.Commands;
using backendNetCore.Tracking.Domain.Model.ValueObjects;
using backendNetCore.Tracking.Interfaces.REST.Resources;

namespace backendNetCore.Tracking.Interfaces.REST.Transform;

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