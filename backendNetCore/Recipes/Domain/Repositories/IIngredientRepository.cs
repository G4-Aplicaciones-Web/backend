using backendNetCore.Recipes.Domain.Model.Aggregates;

namespace backendNetCore.Recipes.Domain.Repositories;

public interface IIngredientRepository
{
    Task<Ingredient> FindByIdAsync(int id);
}