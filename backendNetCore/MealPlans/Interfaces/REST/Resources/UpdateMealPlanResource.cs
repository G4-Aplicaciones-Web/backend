namespace backendNetCore.MealPlans.Interfaces.REST.Resources;

public record UpdateMealPlanResource(string ProfileId, string Summary, int Score);