using System.ComponentModel.DataAnnotations;

namespace backendNetCore.Recipes.Interfaces.REST.Resources;

public record RemoveIngredientFromRecipeResource(
    [Required] int IngredientId
    );