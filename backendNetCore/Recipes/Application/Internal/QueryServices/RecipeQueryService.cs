using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.Queries;
using backendNetCore.Recipes.Domain.Repositories;
using backendNetCore.Recipes.Domain.Services;

namespace backendNetCore.Recipes.Application.Internal.QueryServices;

public class RecipeQueryService(
    IRecipeRepository recipeRepository, 
    IIngredientRepository ingredientRepository
    ) : IRecipeQueryService
{
    public async Task<Recipe?> Handle(GetRecipeByIdQuery query)
    {
        var recipe = await recipeRepository.FindRecipeByIdWithIngredientsAsync(query.RecipeId);
        
        // Si la receta existe y tiene ingredientes, obtén los detalles de esos ingredientes
        if (recipe != null && recipe.Ingredients.Any())
        {
            var ingredientIds = recipe.Ingredients.Select(iq => iq.IngredientId).Distinct().ToList();
            var ingredients = await ingredientRepository.FindByIdsAsync(ingredientIds);
            
            // Adjuntar los ingredientes a la receta de alguna manera para el assembler.
            // Una opción es retornar una tupla o un objeto custom, pero el assembler espera la receta y la lista de ingredientes.
            // Para mantener la firma del IRecipeQueryService, podemos hacer que el Handle retorne Recipe,
            // y el Controller sea responsable de obtener los ingredientes. O, para una encapsulación mejor,
            // el query service podría tener un método que retorne un objeto "enriched" Recipe.

            // Dado que el assembler necesita ambos, la mejor opción es que el controlador
            // orqueste la llamada al repositorio de ingredientes.
            // O podemos cambiar el contrato del Query Service.
        }
        
        return recipe;
    }

    public async Task<IEnumerable<Recipe>> Handle(GetAllRecipesQuery query)
    {
        // Para GetAllRecipes, también querrás cargar los ingredientes si se van a mostrar en la lista
        var recipes = await recipeRepository.ListAsync(); // Asumo que ListAsync no incluye _ingredients
        
        // Si necesitas que ListAsync también cargue los ingredientes (para el RecipeResourceFromEntityAssembler)
        // en una lista de recetas, deberías crear un método ListAllWithIngredientsAsync en IRecipeRepository.
        // Por simplicidad, aquí solo cargaremos la receta principal. Si el front-end pide listas simples,
        // esto es suficiente. Si necesita ver los ingredientes en la lista, sí, necesitarás cargar todo.
        
        // Para cada receta en la lista, y sus ingredientes, tendrías que hacer la misma lógica que GetRecipeById
        // Esto puede ser ineficiente si la lista es grande (problema N+1).
        // Si el front-end necesita todos los detalles de los ingredientes en una lista,
        // se debería optimizar la consulta en el repositorio (ej. con Include y ThenInclude si es posible).
        
        return recipes;
    }

    public async Task<IEnumerable<Recipe>> Handle(SearchRecipesByNameQuery query)
    {
        return await recipeRepository.SearchByNameAsync(query.Name);   
    }

    public async Task<IEnumerable<Recipe>> Handle(GetRecipesByRecipeTypeQuery query)
    {
        return await recipeRepository.FindByRecipeTypeAsync(query.RecipeType);
    }

    public async Task<IEnumerable<Recipe>> Handle(GetRecipesByIngredientQuery query)
    {
        return await recipeRepository.FindByIngredientIdAsync(query.IngredientId);
    }
}