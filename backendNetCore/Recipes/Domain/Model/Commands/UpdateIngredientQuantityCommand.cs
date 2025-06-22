namespace backendNetCore.Recipes.Domain.Model.Commands;

public record UpdateIngredientQuantityCommand(int RecipeId, int IngredientId, double NewQuantity);