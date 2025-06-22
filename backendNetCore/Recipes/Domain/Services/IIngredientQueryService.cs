using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.Queries;

namespace backendNetCore.Recipes.Domain.Services;

public interface IIngredientQueryService
{
    Task<Ingredient?> Handle(GetIngredientByIdQuery query);
    Task<Ingredient?> Handle(GetIngredientByNameQuery query);
    Task<IEnumerable<Ingredient>> Handle(GetAllIngredientsQuery query);
    Task<IEnumerable<Ingredient>> Handle(GetIngredientsByCategoryQuery query);
    Task<IEnumerable<Ingredient>> Handle(GetIngredientsByIdsQuery query);
}