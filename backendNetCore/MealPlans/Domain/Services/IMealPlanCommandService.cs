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
    
    /// <summary>
    /// Handle the update meal plan command
    /// </summary>
    /// <remarks>
    /// This method handles the update meal plan command. It updates an existing meal plan
    /// with the provided data.
    /// </remarks>
    /// <param name="command">The update meal plan command</param>
    /// <returns>The updated meal plan if successful, null otherwise</returns>
    Task<MealPlan?> Handle(UpdateMealPlanCommand command);
    
    /// <summary>
    /// Handle the delete meal plan command
    /// </summary>
    /// <remarks>
    /// This method handles the delete meal plan command. It removes an existing meal plan
    /// from the system.
    /// </remarks>
    /// <param name="command">The delete meal plan command</param>
    /// <returns>True if deletion was successful, false otherwise</returns>
    Task<bool> Handle(DeleteMealPlanCommand command);
}