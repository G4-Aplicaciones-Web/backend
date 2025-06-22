using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.Entities;
using backendNetCore.Recipes.Interfaces.REST.Resources;

namespace backendNetCore.Recipes.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create an IngredientQuantityResource from an IngredientQuantity entity and its associated Ingredient.
/// </summary>
public static class IngredientQuantityResourceFromEntityAssembler
{
    /// <summary>
    /// Assembles an IngredientQuantityResource from an IngredientQuantity entity and its associated Ingredient.
    /// </summary>
    /// <param name="ingredientQuantity">
    /// The <see cref="IngredientQuantity"/> entity to assemble the resource from.
    /// </param>
    /// <param name="ingredient">
    /// The associated <see cref="Ingredient"/> entity to get the name from.
    /// </param>
    /// <returns>
    /// The <see cref="IngredientQuantityResource"/> resource assembled from the entities.
    /// </returns>
    public static IngredientQuantityResource ToResourceFromEntity(IngredientQuantity ingredientQuantity, Ingredient ingredient)
    {
        // Se asume que 'ingredient' no es nulo y corresponde a 'ingredientQuantity.IngredientId'
        return new IngredientQuantityResource(
            ingredientQuantity.IngredientId,
            ingredientQuantity.Quantity,
            ingredient.Name
        );
    }
}