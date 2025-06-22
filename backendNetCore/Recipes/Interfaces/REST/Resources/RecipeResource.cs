using backendNetCore.Recipes.Domain.Model.ValueObjects;

namespace backendNetCore.Recipes.Interfaces.REST.Resources;

public record RecipeResource(
    int Id,
    string Name,
    string Description,
    string UrlInstructions,
    MacronutrientValues TotalNutrients,
    int UserId,
    ERecipeType RecipeType,
    IEnumerable<IngredientQuantityResource> Ingredients
    );