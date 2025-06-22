using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.ValueObjects;
using backendNetCore.Recipes.Domain.Repositories;
using backendNetCore.Shared.Infrastructure.Persistence.Configuration;
using backendNetCore.Shared.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backendNetCore.Recipes.Infrastructure.Persistence.EFC.Repositories;

public class IngredientRepository(AppDbContext context) : BaseRepository<Ingredient>(context), IIngredientRepository
{
    public async Task<Ingredient?> FindByNameAsync(string name)
    {
        return await Context.Set<Ingredient>()
            .FirstOrDefaultAsync(i => i.Name.Equals(name));
    }

    public async Task<IEnumerable<Ingredient>> FindByCategoryAsync(ECategory category)
    {
        return await Context.Set<Ingredient>()
            .Where(i => i.IngredientCategory == category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ingredient>> FindByIdsAsync(IEnumerable<int> ids)
    {
        return await Context.Set<Ingredient>()
            .Where(i => ids.Contains(i.Id))
            .ToListAsync();
    }
}