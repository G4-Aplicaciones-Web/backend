using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.Commands;

namespace backendNetCore.Recipes.Domain.Services;

public interface IIngredientCommandService
{
    Task<Ingredient?> Handle(CreateIngredientCommand command);
    Task<Ingredient?> Handle(UpdateIngredientCommand command);
}