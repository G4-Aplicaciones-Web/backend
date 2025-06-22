using System.ComponentModel.DataAnnotations;
using backendNetCore.Recipes.Domain.Model.ValueObjects;

namespace backendNetCore.Recipes.Interfaces.REST.Resources;

public record CreateRecipeResource(
    [Required] [StringLength(100, MinimumLength = 1)] string Name,
    [Required] [StringLength(500, MinimumLength = 1)] string Description,
    [Required] int UserId,
    [Required] ERecipeType RecipeType,
    [StringLength(500)] string? UrlInstructions
    );