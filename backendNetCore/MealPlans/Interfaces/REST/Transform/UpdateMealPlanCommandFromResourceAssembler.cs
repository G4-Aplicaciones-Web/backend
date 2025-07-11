using backendNetCore.MealPlans.Domain.Model.Commands;
using backendNetCore.MealPlans.Interfaces.REST.Resources;

namespace backendNetCore.MealPlans.Interfaces.REST.Transform;

public static class UpdateMealPlanCommandFromResourceAssembler
{
    public static UpdateMealPlanCommand ToCommandFromResource(int id, UpdateMealPlanResource resource)
    {
        return new UpdateMealPlanCommand(id, resource.ProfileId, resource.Summary, resource.Score);
    }
}