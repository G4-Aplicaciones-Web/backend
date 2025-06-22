namespace backendNetCore.Recipes.Interfaces.REST.Resources;

public record IngredientQuantityResource(
    int IngredientId,
    double Quantity,
    string IngredientName
    );