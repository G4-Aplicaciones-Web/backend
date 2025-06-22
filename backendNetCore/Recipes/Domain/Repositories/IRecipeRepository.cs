using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.ValueObjects;
using backendNetCore.Shared.Domain.Repositories;

namespace backendNetCore.Recipes.Domain.Repositories;

public interface IRecipeRepository : IBaseRepository<Recipe>
{
    Task<IEnumerable<Recipe>> FindRecipeByUserIdAsync(int userId);
    Task<Recipe?> FindRecipeByIdWithIngredientsAsync(int id);
    Task<IEnumerable<Recipe>> FindByRecipeTypeAsync(ERecipeType recipeType);
    Task<IEnumerable<Recipe>> SearchByNameAsync(string name);
    Task<IEnumerable<Recipe>> FindByIngredientIdAsync(int ingredientId);
}