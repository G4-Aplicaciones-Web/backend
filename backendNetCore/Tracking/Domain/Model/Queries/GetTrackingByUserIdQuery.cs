using backendNetCore.Tracking.Domain.Model.ValueObjects;

namespace backendNetCore.Tracking.Domain.Model.Queries;

public record GetTrackingByUserIdQuery(UserId UserId);