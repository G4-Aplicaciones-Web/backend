namespace backendNetCore.Recipes.Domain.Model.Commands;

public record UpdateIngredientQuantity(int RecipeId, int IngredientId, double NewQuantity);