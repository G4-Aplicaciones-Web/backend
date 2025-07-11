using backendNetCore.Tracking.Domain.Model.Entities;

namespace backendNetCore.Tracking.Interfaces.REST.Resources;

public record UpdateTrackingGoalResource(int UserId, string GoalType);