namespace backendNetCore.Tracking.Interfaces.REST.Resources;

public record CreateTrackingGoalFromProfileResource(
    int UserId  // El userId que será validado en el contexto IAM
);