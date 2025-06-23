using backendNetCore.Shared.Infrastructure.Persistence.Configuration;
using backendNetCore.Shared.Infrastructure.Persistence.Repositories;
using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Model.ValueObjects;
using backendNetCore.Tracking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backendNetCore.Tracking.Infrastructure.Persistence.EFC.Repositories;

public class MealPlanTypeRepository(AppDbContext context)
    : BaseRepository<MealPlanType>(context), IMealPlanTypeRepository
{
    public async Task<bool> ExistsByNameAsync(MealPlanTypes mealPlanType) =>
        await Context.Set<MealPlanType>()
            .AnyAsync(m => m.Name == mealPlanType);

    public async Task<MealPlanType?> FindByNameAsync(MealPlanTypes name)
    {
        return await Context.Set<MealPlanType>()
            .FirstOrDefaultAsync(m => m.Name == name);
    }
}