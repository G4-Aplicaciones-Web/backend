namespace backendNetCore.Tracking.Domain.Model.Entities;

public class TrackingMacronutrient
{
    public int Id { get; set; }
    public double Calories { get; set; }
    public double Carbs { get; set; }
    public double Proteins { get; set; }
    public double Fats { get; set; }

    // Constructor sin parámetros
    public TrackingMacronutrient()
    {
    }

    // Constructor con parámetros
    public TrackingMacronutrient(int id, double calories, double carbs, double proteins, double fats)
    {
        Id = id;
        Calories = calories;
        Carbs = carbs;
        Proteins = proteins;
        Fats = fats;
    }

    // Constructor sin ID (para crear nuevos registros)
    public TrackingMacronutrient(double calories, double carbs, double proteins, double fats)
    {
        Calories = calories;
        Carbs = carbs;
        Proteins = proteins;
        Fats = fats;
    }

    // Método para sumar macronutrientes
    public TrackingMacronutrient Add(TrackingMacronutrient other)
    {
        return new TrackingMacronutrient(
            Calories + other.Calories,
            Carbs + other.Carbs,
            Proteins + other.Proteins,
            Fats + other.Fats
        );
    }

    // Método estático para crear instancia vacía
    public static TrackingMacronutrient Zero()
    {
        return new TrackingMacronutrient(0, 0, 0, 0);
    }
}