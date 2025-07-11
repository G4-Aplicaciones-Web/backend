namespace backendNetCore.Recommendations.Domain.Model.Commands;

public record AutoAssignRecommendationsCommand(
    long UserId,
    int Count = 4
);