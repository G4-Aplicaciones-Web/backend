
using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Model.ValueObjects;

namespace backendNetCore.Tracking.Domain.Model.Commands;

public record UpdateTrackingGoalCommand(UserId UserId, GoalType GoalType);
