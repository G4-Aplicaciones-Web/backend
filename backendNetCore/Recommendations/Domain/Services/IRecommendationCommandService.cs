using backendNetCore.Recommendations.Domain.Model.Aggregates;
using backendNetCore.Recommendations.Domain.Model.Commands;

namespace backendNetCore.Recommendations.Domain.Services;

public interface IRecommendationCommandService
{
    Task<Recommendation> Handle(AssignRecommendationCommand command);
    Task<Recommendation> Handle(UpdateRecommendationCommand command);
    Task<Recommendation> Handle(CreateBaseRecommendationCommand command);
    Task<IEnumerable<Recommendation>> Handle(AutoAssignRecommendationsCommand command);
    Task Handle(DeleteRecommendationCommand command);
}