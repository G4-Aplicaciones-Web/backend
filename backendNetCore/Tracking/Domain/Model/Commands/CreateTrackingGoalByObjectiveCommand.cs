using backendNetCore.Tracking.Domain.Model.ValueObjects;

namespace backendNetCore.Tracking.Domain.Model.Commands;

public record CreateTrackingGoalByObjectiveCommand(UserId UserId, string GoalType);