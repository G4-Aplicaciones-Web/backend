using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Repositories;
using backendNetCore.Shared.Infrastructure.Persistence.Configuration;
using backendNetCore.Shared.Infrastructure.Persistence.Repositories;

namespace backendNetCore.Recipes.Infrastructure.Persistence.EFC.Repositories;

public class IngredientRepository(AppDbContext context)
:BaseRepository<Ingredient>(context), IIngredientRepository
{
    public Task<Ingredient> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}