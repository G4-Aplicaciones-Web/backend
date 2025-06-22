using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.ValueObjects;
using backendNetCore.Shared.Domain.Repositories;

namespace backendNetCore.Recipes.Domain.Repositories;

public interface IIngredientRepository : IBaseRepository<Ingredient>
{
    Task<Ingredient?> FindByNameAsync(string name);
    Task<IEnumerable<Ingredient>> FindByCategoryAsync(ECategory category);
    Task<IEnumerable<Ingredient>> FindByIdsAsync(IEnumerable<int> ids);
}