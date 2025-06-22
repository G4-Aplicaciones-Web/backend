using backendNetCore.Recipes.Domain.Model.Commands;
using backendNetCore.Recipes.Interfaces.REST.Resources;

namespace backendNetCore.Recipes.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create an AddIngredientToRecipeCommand from an AddIngredientToRecipeResource.
/// </summary>
public static class AddIngredientToRecipeCommandFromResourceAssembler
{
    /// <summary>
    /// Assembles an AddIngredientToRecipeCommand from an AddIngredientToRecipeResource.
    /// </summary>
    /// <param name="recipeId">The ID of the recipe to add the ingredient to.</param>
    /// <param name="resource">The <see cref="AddIngredientToRecipeResource"/> resource.</param>
    /// <returns>The <see cref="AddIngredientToRecipeCommand"/> command.</returns>
    public static AddIngredientToRecipeCommand ToCommandFromResource(int recipeId, AddIngredientToRecipeResource resource)
    {
        return new AddIngredientToRecipeCommand(recipeId, resource.IngredientId, resource.Quantity);
    }
}