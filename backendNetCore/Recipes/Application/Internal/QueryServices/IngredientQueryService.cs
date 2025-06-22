using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.Queries;
using backendNetCore.Recipes.Domain.Repositories;
using backendNetCore.Recipes.Domain.Services;

namespace backendNetCore.Recipes.Application.Internal.QueryServices;

public class IngredientQueryService(IIngredientRepository ingredientRepository) : IIngredientQueryService
{
    public async Task<Ingredient?> Handle(GetIngredientByIdQuery query)
    {
        return await ingredientRepository.FindByIdAsync(query.IngredientId);
    }

    public async Task<Ingredient?> Handle(GetIngredientByNameQuery query)
    {
        return await ingredientRepository.FindByNameAsync(query.Name);
    }

    public async Task<IEnumerable<Ingredient>> Handle(GetAllIngredientsQuery query)
    {
        return await ingredientRepository.ListAsync();
    }

    public async Task<IEnumerable<Ingredient>> Handle(GetIngredientsByCategoryQuery query)
    {
        return await ingredientRepository.FindByCategoryAsync(query.Category);
    }
}