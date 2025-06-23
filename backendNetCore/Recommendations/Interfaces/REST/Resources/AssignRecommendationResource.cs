using backendNetCore.Recommendations.Domain.Model.ValueObjects;

namespace backendNetCore.Recommendations.Interfaces.REST.Resources;

public record AssignRecommendationResource(
    long UserId,
    int TemplateId,
    string Reason,
    string Notes,
    TimeOfDay TimeOfDay,
    double Score,
    RecommendationStatus Status
);