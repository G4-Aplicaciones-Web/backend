using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Interfaces.REST.Resources;

namespace backendNetCore.Recipes.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create a RecipeResource from a Recipe entity.
/// </summary>
public static class RecipeResourceFromEntityAssembler
{
    /// <summary>
    /// Assembles a RecipeResource from a Recipe entity.
    /// </summary>
    /// <param name="entity">
    /// The <see cref="Recipe"/> entity to assemble the resource from.
    /// </param>
    /// <param name="allIngredientsInRecipe">
    /// A collection of all <see cref="Ingredient"/> entities related to the recipe's ingredients,
    /// needed to populate names in IngredientQuantityResources.
    /// </param>
    /// <returns>
    /// The <see cref="RecipeResource"/> resource assembled from the entity.
    /// </returns>
    public static RecipeResource ToResourceFromEntity(Recipe entity, IEnumerable<Ingredient> allIngredientsInRecipe)
    {
        // Transformar la colección de IngredientQuantity a IngredientQuantityResource
        var ingredientQuantitiesResources = entity.Ingredients.Select(iq =>
        {
            var ingredient = allIngredientsInRecipe.FirstOrDefault(i => i.Id == iq.IngredientId);
            if (ingredient == null)
            {
                return new IngredientQuantityResource(iq.IngredientId, iq.Quantity);
            }
            return IngredientQuantityResourceFromEntityAssembler.ToResourceFromEntity(iq, ingredient);
        }).ToList();

        return new RecipeResource(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.UrlInstructions,
            entity.TotalNutrients,
            entity.UserId.Id,
            entity.RecipeType,
            ingredientQuantitiesResources
        );
    }
}