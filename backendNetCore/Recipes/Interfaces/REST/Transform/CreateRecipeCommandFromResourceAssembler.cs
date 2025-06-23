using backendNetCore.Recipes.Domain.Model.Commands;
using backendNetCore.Recipes.Domain.Model.ValueObjects;
using backendNetCore.Recipes.Interfaces.REST.Resources;

namespace backendNetCore.Recipes.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create a CreateRecipeCommand from a CreateRecipeResource.
/// </summary>
public static class CreateRecipeCommandFromResourceAssembler
{
    /// <summary>
    /// Assembles a CreateRecipeCommand from a CreateRecipeResource.
    /// </summary>
    /// <param name="resource">The <see cref="CreateRecipeResource"/> resource.</param>
    /// <returns>The <see cref="CreateRecipeCommand"/> command.</returns>
    public static CreateRecipeCommand ToCommandFromResource(CreateRecipeResource resource)
    {
        // El UserId en el recurso es un int, lo convertimos a nuestro Value Object UserId.
        var userId = new UserId(resource.UserId);
        return new CreateRecipeCommand(
            resource.Name, 
            resource.Description, 
            userId, 
            resource.RecipeType, 
            resource.UrlInstructions
        );
    }
}