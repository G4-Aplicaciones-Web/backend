using backendNetCore.Recipes.Domain.Model.ValueObjects;

namespace backendNetCore.Recipes.Domain.Model.Commands;

public record CreateRecipeCommand(string Name, string Description, UserId UserId, ERecipeType RecipeType, string UrlInstructions);