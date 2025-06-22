using System.ComponentModel.DataAnnotations;
using backendNetCore.Recipes.Domain.Model.ValueObjects;

namespace backendNetCore.Recipes.Interfaces.REST.Resources;

public record UpdateRecipeResource (
    [Required] [StringLength(100, MinimumLength = 1)] string Name,
    [Required] [StringLength(500, MinimumLength = 1)] string Description,
    [Required] ERecipeType RecipeType,
    [StringLength(500)] string? UrlInstructions
    );