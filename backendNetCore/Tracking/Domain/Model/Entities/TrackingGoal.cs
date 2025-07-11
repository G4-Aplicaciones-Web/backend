using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Model.ValueObjects;

namespace backendNetCore.Tracking.Domain.Model.Entities;

// TrackingGoal Entity
public class TrackingGoal
{
    public int Id { get; set; }
    public UserId UserId { get; set; }
    public GoalType GoalType { get; set; }
    public TrackingMacronutrient TargetMacros { get; set; }

    // Constructor requerido por EF Core
    public TrackingGoal()
    {
    }

    public TrackingGoal(UserId userId, TrackingMacronutrient targetMacros)
    {
        UserId = userId;
        TargetMacros = targetMacros;
    }
    
    public TrackingGoal(UserId userId, GoalType goalType)
    {
        UserId = userId;
        GoalType = goalType;
        // Crear los macros basados en el tipo de meta
        TargetMacros = new TrackingMacronutrient(
            goalType.Calories,
            goalType.Carbs,
            goalType.Proteins,
            goalType.Fats
        );
    }

    public void UpdateGoalType(GoalType newGoalType)
    {
        GoalType = newGoalType;
        // Actualizar los macros cuando cambie el tipo de meta
        TargetMacros = new TrackingMacronutrient(
            newGoalType.Calories,
            newGoalType.Carbs,
            newGoalType.Proteins,
            newGoalType.Fats
        );
    }

    // Métodos de conveniencia que usan el Value Object
    public double GetCalories() => TargetMacros.Calories;
    public double GetCarbs() => TargetMacros.Carbs;
    public double GetProteins() => TargetMacros.Proteins;
    public double GetFats() => TargetMacros.Fats;
    public string GetDisplayName() => GoalType.DisplayName;
}