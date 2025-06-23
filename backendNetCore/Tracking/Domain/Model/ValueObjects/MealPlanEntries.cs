using backendNetCore.Tracking.Domain.Model.Entities;

namespace backendNetCore.Tracking.Domain.Model.ValueObjects;

public record MealPlanEntries(List<MealPlanEntry> ConsumedMeals)
{
    public MealPlanEntries() : this(new List<MealPlanEntry>()) { }

    public MealPlanEntries(IEnumerable<MealPlanEntry> meals) : this(meals.ToList()) { }

    // Funciones de conteo
    public int TotalMealsCount => ConsumedMeals.Count;
    
    // Métodos de manipulación - Devuelven nuevas instancias (Value Object inmutable)
    public MealPlanEntries AddEntry(MealPlanEntry entry)
    {
        var newList = new List<MealPlanEntry>(ConsumedMeals) { entry };
        return new MealPlanEntries(newList);
    }

    public MealPlanEntries AddEntries(IEnumerable<MealPlanEntry> entries)
    {
        var newList = new List<MealPlanEntry>(ConsumedMeals);
        newList.AddRange(entries);
        return new MealPlanEntries(newList);
    }

    public MealPlanEntries RemoveEntry(MealPlanEntry entry)
    {
        var newList = ConsumedMeals.Where(e => !e.Equals(entry)).ToList();
        return new MealPlanEntries(newList);
    }

    public MealPlanEntries ClearEntries()
    {
        return new MealPlanEntries();
    }
}