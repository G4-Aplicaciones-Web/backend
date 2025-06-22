using backendNetCore.MealPlans.Domain.Model.Aggregates;
using backendNetCore.MealPlans.Domain.Model.Commands;
using backendNetCore.MealPlans.Interfaces.REST.Resources;

namespace backendNetCore.MealPlans.Interfaces.REST.Transform;

/// <summary>
/// Assemble a MealPlanResource from a MealPlan
/// </summary>
public static class MealPlanResourceFromEntityAssembler
{
    public static MealPlanResource ToResourceFromEntity(MealPlan entity) =>
        new MealPlanResource(entity.Id, entity.ProfileId, entity.Summary, entity.Score);
}