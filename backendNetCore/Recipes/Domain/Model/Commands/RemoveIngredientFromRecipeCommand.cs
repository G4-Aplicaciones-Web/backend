namespace backendNetCore.Recipes.Domain.Model.Commands;

public record RemoveIngredientFromRecipeCommand(int RecipeId, int IngredientId);