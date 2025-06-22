using AlimentateplusPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using AlimentateplusPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;
using AlimentateplusPlatform.API.Tracking.Domain.Model.ValueObjects;
using AlimentateplusPlatform.API.Tracking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AlimentateplusPlatform.API.Tracking.Infrastructure.Persistence.EFC.Repositories;

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