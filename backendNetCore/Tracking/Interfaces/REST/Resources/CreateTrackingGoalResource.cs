namespace AlimentateplusPlatform.API.Tracking.Interfaces.REST.Resources;

public record CreateTrackingGoalResource(int UserId,
    CreateTrackingMacronutrientResource TargetMacros);