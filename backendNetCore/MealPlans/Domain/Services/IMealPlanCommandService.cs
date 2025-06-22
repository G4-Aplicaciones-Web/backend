using backendNetCore.MealPlans.Domain.Model.Aggregates;
using backendNetCore.MealPlans.Domain.Model.Commands;

namespace backendNetCore.MealPlans.Domain.Services;

public interface IMealPlanCommandService
{
    /// <summary>
    /// Handle the create meal plan command
    /// </summary>
    /// <<remarks>
    /// This method handles the create meal plan command. it checks if the mreal plan already exists
    /// for the given ProfileId, Summary and Score.
    /// </remarks>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<MealPlan?> Handle(CreateMealPlanCommand command);
}