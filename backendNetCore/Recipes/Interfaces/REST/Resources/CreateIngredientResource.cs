using System.ComponentModel.DataAnnotations;
using backendNetCore.Recipes.Domain.Model.ValueObjects;

namespace backendNetCore.Recipes.Interfaces.REST.Resources;

public record CreateIngredientResource(
    [Required] [StringLength(100, MinimumLength = 1)] string Name,
    [Required] MacronutrientValues Nutrients, // Se asume que MacronutrientValues es un tipo que puede ser deserializado directamente
    [Required] ECategory Category
    );