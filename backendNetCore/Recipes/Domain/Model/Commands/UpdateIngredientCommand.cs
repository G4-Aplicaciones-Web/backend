using backendNetCore.Recipes.Domain.Model.ValueObjects;

namespace backendNetCore.Recipes.Domain.Model.Commands;

public record UpdateIngredientCommand(int Id, string Name, MacronutrientValues Nutrients, ECategory Category);