using backendNetCore.Recommendations.Domain.Model.Aggregates;
using backendNetCore.Recommendations.Domain.Model.Commands;

namespace backendNetCore.Recommendations.Domain.Services;

public interface IRecommendationCommandService
{
    Task<Recommendation> Handle(AssignRecommendationCommand command);
    Task Handle(DeleteRecommendationCommand command);
}