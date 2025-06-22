using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.Queries;
using backendNetCore.Recipes.Domain.Repositories;
using backendNetCore.Recipes.Domain.Services;

namespace backendNetCore.Recipes.Application.Internal.QueryServices;

public class RecipeQueryService(IRecipeRepository recipeRepository) : IRecipeQueryService
{
    public async Task<Recipe?> Handle(GetRecipeByIdQuery query)
    {
        return await recipeRepository.FindByIdAsync(query.RecipeId);
    }

    public async Task<IEnumerable<Recipe>> Handle(GetAllRecipesQuery query)
    {
        return await recipeRepository.ListAsync();
    }

    public async Task<IEnumerable<Recipe>> Handle(SearchRecipesByNameQuery query)
    {
        return await recipeRepository.SearchByNameAsync(query.Name);   
    }

    public async Task<IEnumerable<Recipe>> Handle(GetRecipesByRecipeTypeQuery query)
    {
        return await recipeRepository.FindByRecipeTypeAsync(query.RecipeType);
    }

    public async Task<IEnumerable<Recipe>> Handle(GetRecipesByIngredientQuery query)
    {
        return await recipeRepository.FindByIngredientIdAsync(query.IngredientId);
    }
}