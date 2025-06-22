namespace backendNetCore.MealPlans.Interfaces.REST.Resources;

/// <summary>
///  Represents the data required to create a meal plan
/// </summary>
/// <param name="ProfileId"></param>
/// <param name="Summary"></param>
/// <param name="Score"></param>
public record CreateMealPlanResource(string ProfileId, string Summary, int Score);