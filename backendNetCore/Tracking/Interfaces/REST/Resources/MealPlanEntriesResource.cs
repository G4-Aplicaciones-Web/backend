namespace AlimentateplusPlatform.API.Tracking.Interfaces.REST.Resources;

public record MealPlanEntriesResource(
    long Id,
    long RecipeId,
    string MealPlanType,
    int DayNumber);