using backendNetCore.Tracking.Domain.Model.Commands;
using backendNetCore.Tracking.Domain.Model.ValueObjects;
using backendNetCore.Tracking.Interfaces.REST.Resources;

namespace backendNetCore.Tracking.Interfaces.REST.Transform;

public class CreateMealPlanEntryCommandFromResourceAssembler
{
    public static CreateMealPlanEntryToTrackingCommand ToCommand(CreateMealPlanEntryResource resource, long trackingId)
    {
        if (!Enum.TryParse<MealPlanTypes>(resource.MealPlanType, true, out var mealPlanType))
            throw new ArgumentException("Invalid meal plan type");

        return new CreateMealPlanEntryToTrackingCommand(
            new UserId(resource.UserId),
            trackingId,
            new RecipeId(resource.RecipeId),
            mealPlanType,
            resource.DayNumber
        );
    }
}