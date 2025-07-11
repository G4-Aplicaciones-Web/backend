using backendNetCore.Recommendations.Domain.Model.Aggregates;

namespace backendNetCore.Recommendations.Domain.Model.Repositories;

public interface IRecommendationRepository
{
    Task<IEnumerable<Recommendation>> GetAllAsync();
    Task<Recommendation?> GetByIdAsync(int id);
    Task<IEnumerable<Recommendation>> GetByUserIdAsync(long userId);
    Task<IEnumerable<Recommendation>> GetBaseRecommendationsAsync();
    Task AddAsync(Recommendation recommendation);
    Task UpdateAsync(Recommendation recommendation);
    Task RemoveAsync(Recommendation recommendation);
    Task DeleteAsync(Recommendation recommendation);
}
