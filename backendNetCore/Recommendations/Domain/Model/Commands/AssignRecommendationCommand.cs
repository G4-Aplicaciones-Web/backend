using backendNetCore.Recommendations.Domain.Model.ValueObjects;

namespace backendNetCore.Recommendations.Domain.Model.Commands;

public record AssignRecommendationCommand(
    long UserId,
    int TemplateId,
    string Reason,
    string Notes,
    TimeOfDay TimeOfDay,
    double Score,
    RecommendationStatus Status
);