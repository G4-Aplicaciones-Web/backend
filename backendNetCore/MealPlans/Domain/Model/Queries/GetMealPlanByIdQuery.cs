namespace backendNetCore.MealPlans.Domain.Model.Queries;

/// <summary>
///  Query to get a meal plan by id
/// </summary>
/// <param name="Id">The ID to search</param>

public record GetMealPlanByIdQuery(int Id);