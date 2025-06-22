namespace AlimentateplusPlatform.API.Tracking.Interfaces.REST.Resources;

public record TrackingGoalResource(long Id,
    long UserId,
    TrackingMacronutrientResource TargetMacros);