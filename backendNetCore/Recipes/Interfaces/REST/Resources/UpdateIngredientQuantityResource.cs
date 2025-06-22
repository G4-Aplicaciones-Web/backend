using System.ComponentModel.DataAnnotations;

namespace backendNetCore.Recipes.Interfaces.REST.Resources;

public record UpdateIngredientQuantityResource(
    [Required] int IngredientId,
    [Required] [Range(0.01, double.MaxValue)] double NewQuantity
    );