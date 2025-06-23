namespace backendNetCore.Tracking.Interfaces.REST.Resources;

public record CreateTrackingMacronutrientResource( double Calories,
    double Carbs,
    double Proteins,
    double Fats);