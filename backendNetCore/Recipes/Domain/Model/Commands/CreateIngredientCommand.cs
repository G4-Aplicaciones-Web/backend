using backendNetCore.Recipes.Domain.Model.ValueObjects;

namespace backendNetCore.Recipes.Domain.Model.Commands;

public record CreateIngredientCommand(string Name, MacronutrientValues Nutrients, ECategory Category);