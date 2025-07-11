namespace backendNetCore.Tracking.Interfaces.REST.Resources;

public record GoalTypeResource(
    string GoalType,
    string DisplayName,
    double Calories,
    double Carbs,
    double Proteins,
    double Fats
);