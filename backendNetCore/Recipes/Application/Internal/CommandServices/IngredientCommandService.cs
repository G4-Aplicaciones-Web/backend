using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.Commands;
using backendNetCore.Recipes.Domain.Repositories;
using backendNetCore.Recipes.Domain.Services;
using backendNetCore.Shared.Domain.Repositories;

namespace backendNetCore.Recipes.Application.Internal.CommandServices;

public class IngredientCommandService(
    IIngredientRepository ingredientRepository,
    IUnitOfWork unitOfWork)
    : IIngredientCommandService
{
    public async Task<Ingredient?> Handle(CreateIngredientCommand command)
    {
        var ingredient = new Ingredient(command.Name, command.Nutrients, command.Category);
        await ingredientRepository.AddAsync(ingredient);
        await unitOfWork.CompleteAsync();
        return ingredient;
    }

    public async Task<Ingredient?> Handle(UpdateIngredientCommand command)
    {
        var ingredient = await ingredientRepository.FindByIdAsync(command.Id);
        
        if (ingredient == null)
        {
            return null;
        }
        
        ingredient.UpdateDetails(command.Name, command.Nutrients, command.Category);
        
        // 4. Marcar el aggregate como modificado en el repositorio (si EF Core no lo hace automáticamente por tracking)
        // Normalmente, EF Core rastrea los cambios si el objeto se recuperó del contexto.
        // ingredientRepository.Update(ingredient); // Esto podría ser redundante si EF Core lo rastrea.
        
        await unitOfWork.CompleteAsync();
        return ingredient;
    }
}