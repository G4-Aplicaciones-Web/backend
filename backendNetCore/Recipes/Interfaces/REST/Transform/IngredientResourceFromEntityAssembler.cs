using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Interfaces.REST.Resources;

namespace backendNetCore.Recipes.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create an IngredientResource from an Ingredient entity.
/// </summary>
public static class IngredientResourceFromEntityAssembler
{
    /// <summary>
    /// Assembles an IngredientResource from an Ingredient entity.
    /// </summary>
    /// <param name="entity">
    /// The <see cref="Ingredient"/> entity to assemble the resource from.
    /// </param>
    /// <returns>
    /// The <see cref="IngredientResource"/> resource assembled from the entity.
    /// </returns>
    public static IngredientResource ToResourceFromEntity(Ingredient entity)
    {
        return new IngredientResource(
            entity.Id,
            entity.Name,
            entity.Nutrients, // MacronutrientValues es un record, se puede pasar directamente
            entity.IngredientCategory
        );
    }
}