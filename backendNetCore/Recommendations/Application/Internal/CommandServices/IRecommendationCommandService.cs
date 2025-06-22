using backendNetCore.Recommendations.Domain.Model.Commands;
using backendNetCore.Recommendations.Domain.Model.Aggregates;

namespace backendNetCore.Recommendations.Application.Internal.CommandServices;

public interface IRecommendationCommandService
{
    Task<Recommendation> Handle(AssignRecommendationCommand command);
    Task Handle(DeleteRecommendationCommand command);
    
    Task<bool> DeleteAsync(int id);

}