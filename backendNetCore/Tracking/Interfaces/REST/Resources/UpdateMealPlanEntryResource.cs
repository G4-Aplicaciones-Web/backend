namespace backendNetCore.Tracking.Interfaces.REST.Resources;

public record UpdateMealPlanEntryResource(
    int RecipeId,
    string MealPlanType,
    int DayNumber);
