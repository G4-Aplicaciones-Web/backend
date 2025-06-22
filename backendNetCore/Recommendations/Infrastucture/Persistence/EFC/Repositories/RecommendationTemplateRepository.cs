using backendNetCore.Recommendations.Domain.Model.Entities;
using backendNetCore.Shared.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace backendNetCore.Recommendations.Infrastructure.Persistence.EFC.repositories;

public class RecommendationTemplateRepository
{
    private readonly AppDbContext _context;

    public RecommendationTemplateRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RecommendationTemplate>> GetAllAsync()
    {
        return await _context.RecommendationTemplates.ToListAsync();
    }

    public async Task AddAsync(RecommendationTemplate template)
    {
        await _context.RecommendationTemplates.AddAsync(template);
        await _context.SaveChangesAsync();
    }
}