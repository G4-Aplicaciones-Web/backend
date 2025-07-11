using backendNetCore.Recommendations.Domain.Model.Aggregates;
using backendNetCore.Recommendations.Domain.Model.Repositories;
using backendNetCore.Shared.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace backendNetCore.Recommendations.Infrastructure.Persistence.EFC.Repositories;

public class RecommendationRepository : IRecommendationRepository
{
    private readonly AppDbContext _context;

    public RecommendationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Recommendation>> GetAllAsync()
    {
        return await _context.Recommendations
            .Include(r => r.Template) // Solo si luego agregas entidad RecommendationTemplate
            .ToListAsync();
    }

    public async Task<Recommendation?> GetByIdAsync(int id)
    {
        return await _context.Recommendations
            .Include(r => r.Template)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Recommendation>> GetByUserIdAsync(long userId)
    {
        return await Task.FromResult(
            _context.Recommendations
                .AsEnumerable()
                .Where(r => r.UserId.Value == userId)
                .ToList()
        );
    }

    public async Task<IEnumerable<Recommendation>> GetBaseRecommendationsAsync()
    {
        return await Task.FromResult(
            _context.Recommendations
                .Include(r => r.Template)
                .AsEnumerable()
                .Where(r => r.UserId.Value == 0)
                .ToList()
        );
    }

    public async Task AddAsync(Recommendation recommendation)
    {
        await _context.Recommendations.AddAsync(recommendation);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Recommendation recommendation)
    {
        _context.Recommendations.Update(recommendation);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(Recommendation recommendation)
    {
        _context.Recommendations.Remove(recommendation);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(Recommendation recommendation)
    {
        _context.Recommendations.Remove(recommendation);
        await _context.SaveChangesAsync();
    }
}
