using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.Commands;

namespace backendNetCore.Recipes.Domain.Services;

public interface IRecipeCommandService
{
    Task<Recipe?> Handle(CreateRecipeCommand command);
    Task<Recipe?> Handle(UpdateRecipeCommand command);
    Task<Recipe?> Handle(DeleteRecipeCommand command);
    Task<Recipe?> Handle(AddIngredientToRecipeCommand command);
    Task<Recipe?> Handle(RemoveIngredientFromRecipeCommand command);
    Task<Recipe?> Handle(UpdateIngredientQuantityCommand command);
}