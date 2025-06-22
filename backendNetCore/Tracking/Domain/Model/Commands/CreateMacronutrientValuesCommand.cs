namespace backendNetCore.API.Tracking.Domain.Model.Commands;

public record CreateMacronutrientValuesCommand(int Id, double Calories, double Carbs, double Proteins, double Fats);