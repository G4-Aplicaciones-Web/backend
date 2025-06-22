using backendNetCore.Recipes.Domain.Model.ValueObjects;

namespace backendNetCore.Recipes.Domain.Model.Commands;

public record UpdateRecipeCommand(int Id, string Name, string Description, ERecipeType RecipeType, string UrlInstructions);