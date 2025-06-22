using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.Queries;

namespace backendNetCore.Recipes.Domain.Services;

public interface IRecipeQueryService
{
    Task<Recipe?> Handle(GetRecipeByIdQuery query);
    Task<IEnumerable<Recipe>> Handle(GetAllRecipesQuery query);
    Task<IEnumerable<Recipe>> Handle(SearchRecipesByNameQuery query);
    Task<IEnumerable<Recipe>> Handle(GetRecipesByRecipeTypeQuery query);
    Task<IEnumerable<Recipe>> Handle(GetRecipesByIngredientQuery query);
}