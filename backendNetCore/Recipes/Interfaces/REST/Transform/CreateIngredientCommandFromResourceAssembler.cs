using backendNetCore.Recipes.Domain.Model.Commands;
using backendNetCore.Recipes.Interfaces.REST.Resources;

namespace backendNetCore.Recipes.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create a CreateIngredientCommand from a CreateIngredientResource.
/// </summary>
public static class CreateIngredientCommandFromResourceAssembler
{
    /// <summary>
    /// Assembles a CreateIngredientCommand from a CreateIngredientResource.
    /// </summary>
    /// <param name="resource">The <see cref="CreateIngredientResource"/> resource.</param>
    /// <returns>The <see cref="CreateIngredientCommand"/> command.</returns>
    public static CreateIngredientCommand ToCommandFromResource(CreateIngredientResource resource)
    {
        return new CreateIngredientCommand(resource.Name, resource.Nutrients, resource.Category);
    }
}