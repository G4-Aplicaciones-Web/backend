namespace backendNetCore.Tracking.Domain.Model.Entities;

public class GoalType : IEquatable<GoalType>
{
    public static readonly GoalType Mantenimiento = new("Mantenimiento", 2000.0, 250.0, 125.0, 55.6);
    public static readonly GoalType PerdidaPeso = new("Perdida peso", 1500.0, 150.0, 131.25, 41.7);
    public static readonly GoalType GananciaMuscular = new("Ganancia muscular", 2500.0, 343.75, 156.25, 55.6);

    public string DisplayName { get; }
    public double Calories { get; }
    public double Carbs { get; }
    public double Proteins { get; }
    public double Fats { get; }

    private GoalType(string displayName, double calories, double carbs, double proteins, double fats)
    {
        DisplayName = displayName;
        Calories = calories;
        Carbs = carbs;
        Proteins = proteins;
        Fats = fats;
    }

    public static IEnumerable<GoalType> GetAll() => new[] { Mantenimiento, PerdidaPeso, GananciaMuscular };

    public static GoalType FromDisplayName(string displayName)
    {
        return GetAll().FirstOrDefault(gt => gt.DisplayName.Equals(displayName, StringComparison.OrdinalIgnoreCase))
               ?? throw new ArgumentException($"Tipo de meta no válido: {displayName}");
    }

    // Implementación de IEquatable
    public bool Equals(GoalType other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return DisplayName == other.DisplayName;
    }

    public override bool Equals(object obj) => Equals(obj as GoalType);
    public override int GetHashCode() => DisplayName.GetHashCode();
    public static bool operator ==(GoalType left, GoalType right) => Equals(left, right);
    public static bool operator !=(GoalType left, GoalType right) => !Equals(left, right);
    public override string ToString() => DisplayName;
}