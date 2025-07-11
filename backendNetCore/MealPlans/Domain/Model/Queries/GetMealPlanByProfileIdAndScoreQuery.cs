namespace backendNetCore.MealPlans.Domain.Model.Queries;

/// <summary>
/// Query to get a meal plan by ProfileId
/// </summary>
/// <param name="ProfileId"></param>
/// <param name="Score"></param>
public record GetMealPlanByProfileIdAndScoreQuery(int ProfileId, int Score);