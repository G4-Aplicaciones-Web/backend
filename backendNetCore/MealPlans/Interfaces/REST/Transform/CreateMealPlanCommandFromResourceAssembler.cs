using backendNetCore.MealPlans.Domain.Model.Commands;
using backendNetCore.MealPlans.Interfaces.REST.Resources;

namespace backendNetCore.MealPlans.Interfaces.REST.Transform;

/// <summary>
/// Assembles a CreateMealPlanCommand from a CreateMealPlanResource
/// </summary>
public static class CreateMealPlanCommandFromResourceAssembler
{
    public static CreateMealPlanCommand
        ToCommandFromResource(CreateMealPlanResource resource) => 
        new CreateMealPlanCommand(resource.ProfileId,resource.Summary, resource.Score);
    
}