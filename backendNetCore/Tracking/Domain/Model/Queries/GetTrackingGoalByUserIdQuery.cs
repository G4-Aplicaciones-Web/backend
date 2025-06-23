using backendNetCore.Tracking.Domain.Model.ValueObjects;

namespace backendNetCore.Tracking.Domain.Model.Queries;

public record GetTrackingGoalByUserIdQuery(UserId UserId);

