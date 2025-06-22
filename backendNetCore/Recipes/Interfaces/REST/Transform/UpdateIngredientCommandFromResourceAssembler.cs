using backendNetCore.Recipes.Domain.Model.Commands;
using backendNetCore.Recipes.Interfaces.REST.Resources;

namespace backendNetCore.Recipes.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create an UpdateIngredientCommand from an UpdateIngredientResource.
/// </summary>
public static class UpdateIngredientCommandFromResourceAssembler
{
    /// <summary>
    /// Assembles an UpdateIngredientCommand from an UpdateIngredientResource.
    /// </summary>
    /// <param name="ingredientId">The ID of the ingredient to update.</param>
    /// <param name="resource">The <see cref="UpdateIngredientResource"/> resource.</param>
    /// <returns>The <see cref="UpdateIngredientCommand"/> command.</returns>
    public static UpdateIngredientCommand ToCommandFromResource(int ingredientId, UpdateIngredientResource resource)
    {
        return new UpdateIngredientCommand(
            ingredientId, 
            resource.Name, 
            resource.Nutrients, 
            resource.Category
        );
    }
}