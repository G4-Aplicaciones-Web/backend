using backendNetCore.Recipes.Domain.Model.Commands;
using backendNetCore.Recipes.Interfaces.REST.Resources;

namespace backendNetCore.Recipes.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create a RemoveIngredientFromRecipeCommand from a RemoveIngredientFromRecipeResource.
/// </summary>
public static class RemoveIngredientFromRecipeCommandFromResourceAssembler
{
    /// <summary>
    /// Assembles a RemoveIngredientFromRecipeCommand from a RemoveIngredientFromRecipeResource.
    /// </summary>
    /// <param name="recipeId">The ID of the recipe to remove the ingredient from.</param>
    /// <param name="resource">The <see cref="RemoveIngredientFromRecipeResource"/> resource.</param>
    /// <returns>The <see cref="RemoveIngredientFromRecipeCommand"/> command.</returns>
    public static RemoveIngredientFromRecipeCommand ToCommandFromResource(int recipeId, RemoveIngredientFromRecipeResource resource)
    {
        return new RemoveIngredientFromRecipeCommand(recipeId, resource.IngredientId);
    }
}