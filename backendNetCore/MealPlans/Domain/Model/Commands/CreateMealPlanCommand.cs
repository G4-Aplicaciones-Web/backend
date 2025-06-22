namespace backendNetCore.MealPlans.Domain.Model.Commands;
/// <summary>
/// Command to create a new meal plan
/// </summary>
/// <param name="ProfileId">The ProfileId pf the meal plans </param>
/// <param name="Summary">The summary of the meal plan</param>
/// <param name="Score">The meal plan score</param>
public record CreateMealPlanCommand(string ProfileId, string Summary, int Score);