namespace backendNetCore.Tracking.Interfaces.REST.Resources;

public record UpdateMealPlanEntryResource(  int TrackingId,
    int RecipeId,
    string MealPlanType,
    int DayNumber);