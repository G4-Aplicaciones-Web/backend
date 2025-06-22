using backendNetCore.MealPlans.Domain.Model.Aggregates;
using backendNetCore.MealPlans.Domain.Repositories;
using backendNetCore.Shared.Infrastructure.Persistence.Configuration;
using backendNetCore.Shared.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backendNetCore.MealPlans.Infrastructure.Repositories;

public class MealPlanRepository(AppDbContext context)
: BaseRepository<MealPlan>(context), IMealPlanRepository
{
    public async Task<IEnumerable<MealPlan>> FindByProfileIdAsync(string profileId)
    {
        return await Context.Set<MealPlan>()
            .Where(m => m.ProfileId == profileId).ToListAsync();
    }

    public async Task<MealPlan?> FindByProfileIdAndScoreAsync(string profileId, int score)
    {
        return await Context.Set<MealPlan>()
            .FirstOrDefaultAsync(m => m.ProfileId == profileId && m.Score == score);
    }
}