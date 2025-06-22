namespace backendNetCore.Recipes.Domain.Model.Commands;

public record AddIngredientToRecipeCommand(int RecipeId, int IngredientId, double Quantity);