namespace backendNetCore.MealPlans.Interfaces.REST.Resources;

public record UpdateMealPlanResource(int ProfileId, string Summary, int Score);