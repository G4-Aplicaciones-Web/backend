using backendNetCore.Recommendations.Domain.Model.Aggregates;
using backendNetCore.Recommendations.Domain.Model.Repositories;
using backendNetCore.Recommendations.Application.Internal.QueryServices;

namespace backendNetCore.Recommendations.Application.Internal.QueryServices;

public class RecommendationQueryService : IRecommendationQueryService
{
    private readonly IRecommendationRepository _recommendationRepository;

    public RecommendationQueryService(IRecommendationRepository recommendationRepository)
    {
        _recommendationRepository = recommendationRepository;
    }

    public async Task<IEnumerable<Recommendation>> GetAllAsync()
    {
        return await _recommendationRepository.GetAllAsync();
    }

    public async Task<Recommendation?> GetByIdAsync(int id)
    {
        return await _recommendationRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Recommendation>> GetByUserIdAsync(long userId)
    {
        return await _recommendationRepository.GetByUserIdAsync(userId);
    }
}