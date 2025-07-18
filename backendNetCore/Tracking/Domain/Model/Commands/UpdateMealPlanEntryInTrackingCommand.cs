﻿using backendNetCore.Tracking.Domain.Model.ValueObjects;

namespace backendNetCore.Tracking.Domain.Model.Commands;

/// <summary>
/// Update Meal Plan Entry in Tracking Command
/// </summary>
/// <param name="TrackingId">
/// The ID of the tracking aggregate containing the meal plan entry
/// </param>
/// <param name="MealPlanEntryId">
/// The ID of the meal plan entry to update
/// </param>
/// <param name="NewRecipeId">
/// The new recipe ID for the meal plan entry
/// </param>
/// <param name="NewMealType">
/// The new type of meal (breakfast, lunch, dinner, snack, etc.)
/// </param>
/// <param name="NewDayNumber">
/// The new day number for the meal plan entry
/// </param>
public record UpdateMealPlanEntryInTrackingCommand(
    int MealPlanEntryId,
    RecipeId RecipeId,
    MealPlanTypes MealPlanType,
    int DayNumber);