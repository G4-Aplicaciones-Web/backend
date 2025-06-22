namespace backendNetCore.MealPlans.Domain.Model.Queries;

/// <summary>
/// Query to get all meal plans by ProfileId
/// </summary>
/// <param name="ProfileId"></param>
public record GetAllMealPlanByProfileIdQuery(string ProfileId);