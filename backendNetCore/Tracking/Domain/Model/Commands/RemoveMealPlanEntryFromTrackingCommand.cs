namespace backendNetCore.Tracking.Domain.Model.Commands;

/// <summary>
/// Remove Meal Plan Entry from Tracking Command
/// </summary>
/// <param name="TrackingId">
/// The ID of the tracking aggregate to remove the meal plan entry from
/// </param>
/// <param name="MealPlanEntryId">
/// The ID of the meal plan entry to remove
/// </param>
public record RemoveMealPlanEntryFromTrackingCommand(int TrackingId, int MealPlanEntryId);