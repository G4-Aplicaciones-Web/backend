using backendNetCore.Tracking.Domain.Model.ValueObjects;

namespace backendNetCore.Tracking.Domain.Model.Commands;

/// <summary>
/// Add Meal Plan Entry to Tracking Command
/// </summary>
/// <param name="TrackingId">
/// The ID of the tracking aggregate to add the meal plan entry to
/// </param>
/// <param name="RecipeId">
/// The ID of the recipe for the meal plan entry
/// </param>
/// <param name="MealType">
/// The type of meal (breakfast, lunch, dinner, snack, etc.)
/// </param>
/// <param name="DayNumber">
/// The day number for the meal plan entry
/// </param>
public record CreateMealPlanEntryToTrackingCommand(
    UserId UserId,
    long TrackingId,
    RecipeId RecipeId,
    MealPlanTypes MealPlanType,
    int DayNumber);