using backendNetCore.Recommendations.Domain.Model.Aggregates;

namespace backendNetCore.Recommendations.Application.Internal.QueryServices;

public interface IRecommendationQueryService
{
    Task<IEnumerable<Recommendation>> GetAllAsync();
    Task<Recommendation?> GetByIdAsync(int id);
    Task<IEnumerable<Recommendation>> GetByUserIdAsync(long userId);
}