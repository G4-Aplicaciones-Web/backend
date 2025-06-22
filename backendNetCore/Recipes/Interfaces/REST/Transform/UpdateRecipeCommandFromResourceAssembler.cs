using backendNetCore.Recipes.Domain.Model.Commands;
using backendNetCore.Recipes.Interfaces.REST.Resources;

namespace backendNetCore.Recipes.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create an UpdateRecipeCommand from an UpdateRecipeResource.
/// </summary>
public static class UpdateRecipeCommandFromResourceAssembler
{
    /// <summary>
    /// Assembles an UpdateRecipeCommand from an UpdateRecipeResource.
    /// </summary>
    /// <param name="recipeId">The ID of the recipe to update.</param>
    /// <param name="resource">The <see cref="UpdateRecipeResource"/> resource.</param>
    /// <returns>The <see cref="UpdateRecipeCommand"/> command.</returns>
    public static UpdateRecipeCommand ToCommandFromResource(int recipeId, UpdateRecipeResource resource)
    {
        return new UpdateRecipeCommand(
            recipeId, 
            resource.Name, 
            resource.Description, 
            resource.RecipeType, 
            resource.UrlInstructions
        );
    }
}