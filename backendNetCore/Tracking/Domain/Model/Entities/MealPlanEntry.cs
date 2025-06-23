using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Model.ValueObjects;

namespace backendNetCore.Tracking.Domain.Model.Entities;

// Entity: MealPlanEntry
/// <summary>
/// Entity representing a meal plan entry for a specific day
/// </summary>
public class MealPlanEntry
{
    public int Id { get; set; }
    public RecipeId RecipeId { get; private set; }
    public MealPlanType MealPlanType { get; private set; }
    public int DayNumber { get; private set; }
    public int TrackingId { get; private set; }
    
    // 🔥 Propiedad de navegación necesaria para evitar el error
    public Aggregates.Tracking Tracking { get; private set; }
    
    protected MealPlanEntry() { } // ← EF Core lo necesita
    
    public MealPlanEntry(RecipeId recipeId, MealPlanType mealPlanType, int dayNumber)
    {
        RecipeId = recipeId;
        MealPlanType = mealPlanType;
        DayNumber = dayNumber;
    }

    public void SetTrackingId(int trackingId)
    {
        if (trackingId <= 0)
            throw new ArgumentException("Tracking ID must be positive", nameof(trackingId));

        TrackingId = trackingId;
    }
}
