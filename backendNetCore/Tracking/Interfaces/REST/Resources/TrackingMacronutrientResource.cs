namespace backendNetCore.Tracking.Interfaces.REST.Resources;

public record TrackingMacronutrientResource(long Id,
    double Calories,
    double Carbs,
    double Proteins,
    double Fats);