using System.ComponentModel.DataAnnotations;
using backendNetCore.Recipes.Domain.Model.ValueObjects;

namespace backendNetCore.Recipes.Interfaces.REST.Resources;

public record UpdateIngredientResource(
    [Required] [StringLength(100, MinimumLength = 1)] string Name,
    [Required] MacronutrientValues Nutrients,
    [Required] ECategory Category
    );