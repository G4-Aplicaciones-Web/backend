using backendNetCore.Recipes.Domain.Model.Commands;
using backendNetCore.Recipes.Interfaces.REST.Resources;

namespace backendNetCore.Recipes.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create an UpdateIngredientQuantityCommand from an UpdateIngredientQuantityResource.
/// </summary>
public static class UpdateIngredientQuantityCommandFromResourceAssembler
{
    /// <summary>
    /// Assembles an UpdateIngredientQuantityCommand from an UpdateIngredientQuantityResource.
    /// </summary>
    /// <param name="recipeId">The ID of the recipe containing the ingredient.</param>
    /// <param name="resource">The <see cref="UpdateIngredientQuantityResource"/> resource.</param>
    /// <returns>The <see cref="UpdateIngredientQuantityCommand"/> command.</returns>
    public static UpdateIngredientQuantityCommand ToCommandFromResource(int recipeId, UpdateIngredientQuantityResource resource)
    {
        // El IngredientId ya viene en el recurso
        return new UpdateIngredientQuantityCommand(recipeId, resource.IngredientId, resource.NewQuantity);
    }
}