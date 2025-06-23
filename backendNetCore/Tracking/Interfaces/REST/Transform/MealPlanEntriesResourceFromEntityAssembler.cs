using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Interfaces.REST.Resources;

namespace backendNetCore.Tracking.Interfaces.REST.Transform;

public class MealPlanEntriesResourceFromEntityAssembler
{
    public static MealPlanEntriesResource ToResource(MealPlanEntry entry)
    {
        var mealPlanTypeName = entry.MealPlanType?.Name.ToString() ?? "Unknown";

        return new MealPlanEntriesResource(
            entry.Id,
            entry.RecipeId.Id,
            mealPlanTypeName,
            entry.DayNumber
        );
    }
}