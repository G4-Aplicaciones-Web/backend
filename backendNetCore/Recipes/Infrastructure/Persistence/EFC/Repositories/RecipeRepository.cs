using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.ValueObjects;
using backendNetCore.Recipes.Domain.Repositories;
using backendNetCore.Shared.Infrastructure.Persistence.Configuration;
using backendNetCore.Shared.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backendNetCore.Recipes.Infrastructure.Persistence.EFC.Repositories;

public class RecipeRepository(AppDbContext context) : BaseRepository<Recipe>(context), IRecipeRepository
{
    public async Task<IEnumerable<Recipe>> FindRecipeByUserIdAsync(int userId)
    {
        var userIdValueObject = new UserId(userId);
        
        return await Context.Set<Recipe>()
            .Include("_ingredients") // Include the ingredients
            .Where(r => r.UserId == userIdValueObject)
            .ToListAsync();
    }

    public async Task<Recipe?> FindRecipeByIdWithIngredientsAsync(int id)
    {
        return await Context.Set<Recipe>()
            .Include("_ingredients")
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Recipe>> FindByRecipeTypeAsync(ERecipeType recipeType)
    {
        return await Context.Set<Recipe>()
            .Include("_ingredients")
            .Where(r => r.RecipeType == recipeType)
            .ToListAsync();
    }

    public async Task<IEnumerable<Recipe>> SearchByNameAsync(string name)
    {
        return await Context.Set<Recipe>()
            .Include("_ingredients") // Generalmente querrás incluir ingredientes en las consultas de recetas
            .Where(r => r.Name.Contains(name))
            .ToListAsync();
    }

    public async Task<IEnumerable<Recipe>> FindByIngredientIdAsync(int ingredientId)
    {
        return await Context.Set<Recipe>()
            .Include("_ingredients")
            .Where(r => r.Ingredients.Any(iq => iq.IngredientId == ingredientId)) // Accedemos a la propiedad pública Ingredients
            .ToListAsync();
    }
}