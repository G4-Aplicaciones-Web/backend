using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.Commands;
using backendNetCore.Recipes.Domain.Model.ValueObjects;
using backendNetCore.Recipes.Domain.Repositories;
using backendNetCore.Recipes.Domain.Services;
using backendNetCore.Shared.Domain.Repositories;

namespace backendNetCore.Recipes.Application.Internal.CommandServices;

public class RecipeCommandService(
    IRecipeRepository recipeRepository,
    IIngredientRepository ingredientRepository,
    IUnitOfWork unitOfWork)
    : IRecipeCommandService
{
    public async Task<Recipe?> Handle(CreateRecipeCommand command)
    {
        // 1. Create a new Recipe aggregate
        var userId = new UserId(command.UserId.Id);
        var recipe = new Recipe(command.Name, command.Description, userId, command.RecipeType, command.UrlInstructions);

        // 2. Add to the repository
        await recipeRepository.AddAsync(recipe);

        // 3. Persist the changes, at first ingredients are not persisted, so there is no need to calculate macronutrients
        await unitOfWork.CompleteAsync();
        
        return recipe;
    }

    public async Task<Recipe?> Handle(UpdateRecipeCommand command)
    {
        var recipe = await recipeRepository.FindByIdAsync(command.Id);
        if (recipe == null)
        {
            return null;
        }
        
        recipe.UpdateDetails(command.Name, command.Description, command.RecipeType, command.UrlInstructions);
        
        await unitOfWork.CompleteAsync();
        return recipe;
    }

    public async Task<Recipe?> Handle(DeleteRecipeCommand command)
    {
        // 1. Find the aggregate
        var recipe = await recipeRepository.FindByIdAsync(command.Id);

        if (recipe == null)
        {
            return null;
        }

        // 2. Mark the aggregate as deleted
        recipeRepository.Remove(recipe);

        // 3. Persist the changes
        await unitOfWork.CompleteAsync();
        
        // 4. Return the aggregate, this will return a null if the aggregate was not found
        return recipe; 
    }

    public async Task<Recipe?> Handle(AddIngredientToRecipeCommand command)
    {
        // 1. Get the recipe, including all its ingredients
        var recipe = await recipeRepository.FindRecipeByIdWithIngredientsAsync(command.RecipeId);
        if (recipe == null)
        {
            return null;
        }

        // 2. Get the ingredient from the repository
        var ingredient = await ingredientRepository.FindByIdAsync(command.IngredientId);
        if (ingredient == null)
        {
            throw new InvalidOperationException($"Ingredient with ID {command.IngredientId} not found.");
        }

        // 3. Add the ingredient to the recipe
        recipe.AddIngredient(ingredient, command.Quantity);

        // 4. Recalculate the total nutrients of the recipe
        // We need *all* the actual Ingredients for all the IngredientQuantity in the recipe
        var ingredientIdsInRecipe = recipe.Ingredients.Select(iq => iq.IngredientId).ToList();
        var allActualIngredients = await ingredientRepository.FindByIdsAsync(ingredientIdsInRecipe);
        
        recipe.RecalculateTotalNutrients(allActualIngredients);

        // 5. Persist the changes
        await unitOfWork.CompleteAsync();

        return recipe;
    }

    public async Task<Recipe?> Handle(RemoveIngredientFromRecipeCommand command)
    {
        // 1. Get the recipe, including all its ingredients.
        var recipe = await recipeRepository.FindRecipeByIdWithIngredientsAsync(command.RecipeId);
        if (recipe == null)
        {
            return null;
        }
        
        // 2. Remove the ingredient from the recipe (domain behavior in the aggregate)
        recipe.RemoveIngredient(command.IngredientId);
        
        // 3. Recalculate the total nutrients of the recipe
        var ingredientIdsInRecipe = recipe.Ingredients.Select(iq => iq.IngredientId).ToList();
        var allActualIngredients = await ingredientRepository.FindByIdsAsync(ingredientIdsInRecipe);
        
        recipe.RecalculateTotalNutrients(allActualIngredients);

        // 4. Persist the changes
        await unitOfWork.CompleteAsync();

        return recipe;
    }

    public async Task<Recipe?> Handle(UpdateIngredientQuantityCommand command)
    {
        // 1. Get the recipe, including all its ingredients.
        var recipe = await recipeRepository.FindRecipeByIdWithIngredientsAsync(command.RecipeId);
        if (recipe == null)
        {
            return null;
        }

        // 2. Update the quantity of the ingredient in the recipe (domain behavior in the aggregate)
        recipe.UpdateIngredientQuantity(command.IngredientId, command.NewQuantity);
        
        // 3. Recalculate the total nutrients of the recipe
        var ingredientIdsInRecipe = recipe.Ingredients.Select(iq => iq.IngredientId).ToList();
        var allActualIngredients = await ingredientRepository.FindByIdsAsync(ingredientIdsInRecipe);
        
        recipe.RecalculateTotalNutrients(allActualIngredients);

        // 4. Persist the changes
        await unitOfWork.CompleteAsync();

        return recipe;
    }
}