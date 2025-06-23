using backendNetCore.Recipes.Domain.Model.ValueObjects;

namespace backendNetCore.Recipes.Interfaces.REST.Resources;

public record IngredientResource(
    int Id,
    string Name,
    MacronutrientValues Nutrients,
    ECategory Category
    );