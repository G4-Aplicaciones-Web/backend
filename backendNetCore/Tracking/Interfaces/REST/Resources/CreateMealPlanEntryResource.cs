namespace AlimentateplusPlatform.API.Tracking.Interfaces.REST.Resources;

public record CreateMealPlanEntryResource( int UserId,
    int RecipeId,
    string MealPlanType,
    int DayNumber);