namespace backendNetCore.Recipes.Domain.Model.ValueObjects;

public record MacronutrientValues
{
    public double Calories { get; init; }
    public double Proteins { get; init; }
    public double Carbohydrates { get; init; }
    public double Fats { get; init; }

    public MacronutrientValues(double calories, double proteins, double carbohydrates, double fats)
    {
        Calories = calories >= 0 ? calories : throw new ArgumentException("Calories cannot be negative");
        Proteins = proteins >= 0 ? proteins : throw new ArgumentException("Proteins cannot be negative");
        Carbohydrates = carbohydrates >= 0 ? carbohydrates : throw new ArgumentException("Carbohydrates cannot be negative");
        Fats = fats >= 0 ? fats : throw new ArgumentException("Fats cannot be negative");
    }

    
    public MacronutrientValues Add(MacronutrientValues other) => new(
        Calories + other.Calories,
        Proteins + other.Proteins,
        Carbohydrates + other.Carbohydrates,
        Fats + other.Fats
    );

    public MacronutrientValues Multiply(double factor) => new(
        Math.Round(Calories * factor, 2),
        Math.Round(Proteins * factor, 2),
        Math.Round(Carbohydrates * factor, 2),
        Math.Round(Fats * factor, 2)
    );
    
    public MacronutrientValues CalculateForQuantity(double quantity, double baseQuantity = 100) =>
        Multiply(quantity / baseQuantity);
    public double GetTotalMacronutrients() => Proteins + Carbohydrates + Fats;

    public double GetProteinPercentage() 
    {
        var total = GetTotalMacronutrients();
        return total > 0 ? Math.Round((Proteins / total) * 100, 1) : 0;
    }

    public double GetCarbsPercentage() 
    {
        var total = GetTotalMacronutrients();
        return total > 0 ? Math.Round((Carbohydrates / total) * 100, 1) : 0;
    }

    public double GetFatsPercentage() 
    {
        var total = GetTotalMacronutrients();
        return total > 0 ? Math.Round((Fats / total) * 100, 1) : 0;
    }

    // Validar si los valores son consistentes
    public bool IsValid()
    {
        // Verificar que las calorías sean consistentes con los macronutrientes
        // 1g proteína = 4 cal, 1g carbohidrato = 4 cal, 1g grasa = 9 cal
        var expectedCalories = (Proteins * 4) + (Carbohydrates * 4) + (Fats * 9);
        var tolerance = Math.Max(expectedCalories * 0.1, 10); // 10% de tolerancia o mínimo 10 calorías
        
        return Math.Abs(Calories - expectedCalories) <= tolerance;
    }
    
    public static MacronutrientValues Empty => new(0, 0, 0, 0);
    
    public override string ToString() => 
        $"Calories: {Calories:F1}, Proteins: {Proteins:F1}g, Carbs: {Carbohydrates:F1}g, Fats: {Fats:F1}g";
}
