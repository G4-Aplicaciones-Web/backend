using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Model.ValueObjects;
using backendNetCore.Tracking.Domain.Model.Commands;

namespace backendNetCore.Tracking.Domain.Model.Aggregates;

// Aggregate Root: Tracking
/// <summary>
/// Tracking Aggregate Root
/// </summary>
/// <remarks>
/// This class represents the Tracking aggregate root for meal planning.
/// It manages the tracking of consumed meals and nutritional information.
/// </remarks>
public class Tracking
{
    private List<MealPlanEntry> _mealPlanEntries = new List<MealPlanEntry>();

    public int Id { get; private set; }
    
    public DateTime Date { get; private set; }
    
    public int TrackingGoalId { get; private set; }
    public int TrackingMacronutrientId { get; private set; }
    public UserId UserProfileId { get; private set; } 
    public TrackingGoal TrackingGoal { get; private set; }
    
    // EF necesita esta propiedad pública
    public List<MealPlanEntry> MealPlanEntriesInternal
    {
        get => _mealPlanEntries;
        private set 
        { 
            _mealPlanEntries = value ?? new List<MealPlanEntry>();
        }
    }
    public TrackingMacronutrient TrackingMacronutrient { get; private set; }
    
    // Value object que encapsula la lógica pero usa la lista interna
    public MealPlanEntries MealPlanEntryList => new MealPlanEntries(_mealPlanEntries ?? new List<MealPlanEntry>());
    

    public Tracking()
    {
        UserProfileId = new UserId(123);
        TrackingGoal = new TrackingGoal();
        TrackingMacronutrient = new TrackingMacronutrient();
        Date = DateTime.Now;
        _mealPlanEntries = new List<MealPlanEntry>();
    }

    public Tracking(UserId userId)
    {
        if (userId.Id <= 0)
            throw new ArgumentException("User profile ID must be positive", nameof(UserProfileId));
        UserProfileId = userId;
        Date = DateTime.Now;
        _mealPlanEntries = new List<MealPlanEntry>();
    }

    public Tracking(UserId userId, DateTime date, TrackingGoal trackingGoal, TrackingMacronutrient trackingMacronutrient)
    {
        if (userId.Id <= 0)
            throw new ArgumentException("User profile ID must be positive", nameof(userId));
        
        UserProfileId = userId;
        Date = date.Date;
        TrackingGoal = trackingGoal ?? throw new ArgumentNullException(nameof(trackingGoal));
        TrackingGoalId = trackingGoal.Id;
        TrackingMacronutrient = trackingMacronutrient ?? throw new ArgumentNullException(nameof(trackingMacronutrient));
        TrackingMacronutrientId = trackingMacronutrient.Id;
        _mealPlanEntries = new List<MealPlanEntry>();
    }

    public void SetTracking(int id)
    {
        Id = id;
    }

    /// <summary>
    /// Load meal plan entries from repository (for EF navigation)
    /// </summary>
    /// <param name="entries">Entries to load</param>
    public void LoadMealPlanEntries(IEnumerable<MealPlanEntry> entries)
    {
        _mealPlanEntries = entries?.ToList() ?? new List<MealPlanEntry>();
    }

    /// <summary>
    /// Add a meal plan entry
    /// </summary>
    /// <param name="mealPlanEntry">Meal plan entry to add</param>
    public void AddMealPlanEntry(MealPlanEntry mealPlanEntry)
    {
        if (mealPlanEntry == null)
            throw new ArgumentNullException(nameof(mealPlanEntry));
        
        _mealPlanEntries ??= new List<MealPlanEntry>();
        _mealPlanEntries.Add(mealPlanEntry);
    }

    /// <summary>
    /// Add multiple meal plan entries
    /// </summary>
    /// <param name="entries">List of meal plan entries to add</param>
    public void AddMealPlanEntries(IList<MealPlanEntry> entries)
    {
        if (entries == null)
            throw new ArgumentNullException(nameof(entries));
        
        _mealPlanEntries ??= new List<MealPlanEntry>();
        _mealPlanEntries.AddRange(entries);
    }

    /// <summary>
    /// Remove a meal plan entry
    /// </summary>
    /// <param name="mealPlanEntry">Meal plan entry to remove</param>
    /// <returns>True if removed successfully, false otherwise</returns>
    public bool RemoveMealPlanEntry(MealPlanEntry mealPlanEntry)
    {
        if (mealPlanEntry == null)
            throw new ArgumentNullException(nameof(mealPlanEntry));
        
        _mealPlanEntries ??= new List<MealPlanEntry>();
        return _mealPlanEntries.Remove(mealPlanEntry);
    }

    /// <summary>
    /// Update a meal plan entry
    /// </summary>
    /// <param name="oldEntry">Old meal plan entry</param>
    /// <param name="newEntry">New meal plan entry</param>
    public void UpdateMealPlanEntry(MealPlanEntry oldEntry, MealPlanEntry newEntry)
    {
        if (oldEntry == null)
            throw new ArgumentNullException(nameof(oldEntry));
        if (newEntry == null)
            throw new ArgumentNullException(nameof(newEntry));
        
        _mealPlanEntries ??= new List<MealPlanEntry>();
        var index = _mealPlanEntries.IndexOf(oldEntry);
        if (index >= 0)
        {
            _mealPlanEntries[index] = newEntry;
        }
    }

    /// <summary>
    /// Clear all meal plan entries
    /// </summary>
    public void ClearMealPlanEntries()
    {
        _mealPlanEntries?.Clear();
    }

    /// <summary>
    /// Get all meal plan entries (for repository access)
    /// </summary>
    public IReadOnlyList<MealPlanEntry> GetMealPlanEntries()
    {
        return _mealPlanEntries.AsReadOnly();
    }
}