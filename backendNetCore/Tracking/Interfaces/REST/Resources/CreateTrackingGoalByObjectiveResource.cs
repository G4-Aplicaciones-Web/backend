namespace backendNetCore.Tracking.Interfaces.REST.Resources;

public record CreateTrackingGoalByObjectiveResource(int UserId, string GoalType);