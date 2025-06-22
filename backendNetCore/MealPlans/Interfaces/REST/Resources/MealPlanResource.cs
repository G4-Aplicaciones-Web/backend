namespace backendNetCore.MealPlans.Interfaces.REST.Resources;
/// <summary>
///  Represents the data provided by the server about a meal plan
/// </summary>
/// <param name="Id"></param>
/// <param name="ProfileId"></param>
/// <param name="Summary"></param>
/// <param name="Score"></param>
public record MealPlanResource(int Id, string ProfileId, string Summary, int Score);