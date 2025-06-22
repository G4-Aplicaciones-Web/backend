using System.Net.Mime;
using backendNetCore.Recipes.Domain.Model.Aggregates;
using backendNetCore.Recipes.Domain.Model.Commands;
using backendNetCore.Recipes.Domain.Model.Queries;
using backendNetCore.Recipes.Domain.Model.ValueObjects;
using backendNetCore.Recipes.Domain.Services;
using backendNetCore.Recipes.Interfaces.REST.Resources;
using backendNetCore.Recipes.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace backendNetCore.Recipes.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Recipes Management Endpoints")]
public class RecipesController(
    IRecipeCommandService recipeCommandService,
    IRecipeQueryService recipeQueryService,
    IIngredientQueryService ingredientQueryService
    ) : ControllerBase
{
    [HttpGet("{recipeId:int}")] // Definir el endpoint HTTP GET con parámetro de ruta
    [SwaggerOperation(
        Summary = "Get a recipe by its ID.",
        Description = "Get a recipe by its ID.",
        OperationId = "GetRecipeById")]
    [SwaggerResponse(200, "Recipe found", typeof(RecipeResource))]
    [SwaggerResponse(404, "Recipe not found")]
    public async Task<IActionResult> GetRecipeById([FromRoute] int recipeId)
    {
        var recipe = await recipeQueryService.Handle(new GetRecipeByIdQuery(recipeId));
        if (recipe is null) return NotFound();
        IEnumerable<Ingredient> allIngredientsInRecipe = new List<Ingredient>();
        if (recipe.Ingredients.Any())
        {
            var ingredientIds = recipe.Ingredients.Select(iq => iq.IngredientId).Distinct();
            

            // Si tienes un GetIngredientsByIdsQuery en Domain.Model.Queries
            // allIngredientsInRecipe = await ingredientQueryService.Handle(new GetIngredientsByIdsQuery(ingredientIds));

            // Si el IngredientQueryService no tiene un método FindByIds, podemos llamar al repositorio directamente aquí,
            // aunque es preferible que la capa de aplicación (QueryService) orqueste esto.
            // Para este ejemplo, lo haremos de la manera más directa para que funcione.
            // ¡Importante!: Esto requiere que IIngredientQueryService tenga un método para buscar por múltiples IDs.
            // Si no lo tiene, deberías agregarlo:
            // IQueryService -> Handle(GetIngredientsByIdsQuery query)
            // GetIngredientsByIdsQuery(IEnumerable<int> ids)
            // IIngredientRepository -> FindByIdsAsync(IEnumerable<int> ids)

            // Asumiendo que ingredientQueryService tiene un método o que podemos extenderlo:
            // Por ahora, para que compile y funcione en el controlador:
            // Si no quieres cambiar el IIngredientQueryService, podrías inyectar IIngredientRepository aquí.
            // Pero es más coherente que el QueryService maneje la obtención de datos.
            // VOY A ASUMIR que tu IIngredientQueryService tendrá un método para esto.
            // Si no lo tiene, el controlador es un buen lugar para inyectar IIngredientRepository si prefieres.

            // Para que funcione, el IngredientQueryService necesita un método para GetIngredientsByIds
            // ¡Así que primero, asegurémonos de que tu IIngredientQueryService soporte esto!
            // Lo definimos previamente en IIngredientQueryService: Task<IEnumerable<Ingredient>> FindByIdsAsync(IEnumerable<int> ids);
            // Ahora, en la implementación de IngredientQueryService, podrías tener:
            // public async Task<IEnumerable<Ingredient>> Handle(GetIngredientsByIdsQuery query) { return await _ingredientRepository.FindByIdsAsync(query.Ids); }
            // Y el GetIngredientsByIdsQuery: public record GetIngredientsByIdsQuery(IEnumerable<int> Ids);

            // Volviendo al controlador:
            allIngredientsInRecipe = await ingredientQueryService.Handle(new GetIngredientsByIdsQuery(ingredientIds)); // <-- ASUMO ESTO
        }
        
        // 3. Transformar la entidad de dominio a un recurso REST utilizando el assembler
        // Pasamos la receta y la colección de ingredientes relacionados.
        var recipeResource = RecipeResourceFromEntityAssembler.ToResourceFromEntity(recipe, allIngredientsInRecipe);
        
        return Ok(recipeResource);
    }
    
    // --- GET /api/v1/Recipes
    [HttpGet]
    [SwaggerOperation("Get all recipes.")]
    [SwaggerResponse(200, "List of recipes", typeof(IEnumerable<RecipeResource>))]
    public async Task<IActionResult> GetAllRecipes()
    {
        var recipes = await recipeQueryService.Handle(new GetAllRecipesQuery());
        
        // Para cada receta, necesitamos los ingredientes asociados para el assembler.
        // Esto es un patrón N+1 si no se carga eficientemente en el repositorio.
        // Para optimizar esto, podrías tener un método ListAllRecipesWithIngredientsAsync en IRecipeRepository
        // que use Include/ThenInclude. Pero por ahora, orquestamos aquí.

        var recipeResources = new List<RecipeResource>();
        foreach (var recipe in recipes)
        {
            IEnumerable<Ingredient> allIngredientsInRecipe = new List<Ingredient>();
            if (recipe.Ingredients.Any())
            {
                var ingredientIds = recipe.Ingredients.Select(iq => iq.IngredientId).Distinct();
                allIngredientsInRecipe = await ingredientQueryService.Handle(new GetIngredientsByIdsQuery(ingredientIds));
            }
            recipeResources.Add(RecipeResourceFromEntityAssembler.ToResourceFromEntity(recipe, allIngredientsInRecipe));
        }

        return Ok(recipeResources);
    }
    
    [HttpGet("search-by-name")]
    [SwaggerOperation("Search recipes by name.")]
    [SwaggerResponse(200, "Matching recipes", typeof(IEnumerable<RecipeResource>))]
    public async Task<IActionResult> SearchRecipesByName([FromQuery] string name)
    {
        var recipes = await recipeQueryService.Handle(new SearchRecipesByNameQuery(name));

        var recipeResources = new List<RecipeResource>();
        foreach (var recipe in recipes)
        {
            IEnumerable<Ingredient> allIngredientsInRecipe = new List<Ingredient>();
            if (recipe.Ingredients.Any())
            {
                var ingredientIds = recipe.Ingredients.Select(iq => iq.IngredientId).Distinct();
                allIngredientsInRecipe = await ingredientQueryService.Handle(new GetIngredientsByIdsQuery(ingredientIds));
            }
            recipeResources.Add(RecipeResourceFromEntityAssembler.ToResourceFromEntity(recipe, allIngredientsInRecipe));
        }

        return Ok(recipeResources);
    }
    
    // --- GET /api/v1/Recipes/by-type/{recipeType}
    [HttpGet("by-type/{recipeType}")]
    [SwaggerOperation("Get recipes by recipe type.")]
    [SwaggerResponse(200, "Matching recipes by type", typeof(IEnumerable<RecipeResource>))]
    public async Task<IActionResult> GetRecipesByRecipeType([FromRoute] ERecipeType recipeType)
    {
        var recipes = await recipeQueryService.Handle(new GetRecipesByRecipeTypeQuery(recipeType));
        
        var recipeResources = new List<RecipeResource>();
        foreach (var recipe in recipes)
        {
            IEnumerable<Ingredient> allIngredientsInRecipe = new List<Ingredient>();
            if (recipe.Ingredients.Any())
            {
                var ingredientIds = recipe.Ingredients.Select(iq => iq.IngredientId).Distinct();
                allIngredientsInRecipe = await ingredientQueryService.Handle(new GetIngredientsByIdsQuery(ingredientIds));
            }
            recipeResources.Add(RecipeResourceFromEntityAssembler.ToResourceFromEntity(recipe, allIngredientsInRecipe));
        }
        return Ok(recipeResources);
    }
    
    [HttpPost]
    [SwaggerOperation("Create a new recipe.")]
    [SwaggerResponse(201, "Recipe created successfully", typeof(RecipeResource))]
    [SwaggerResponse(400, "Invalid request payload")]
    public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeResource resource)
    {
        // 1. Validar el modelo automáticamente por ASP.NET Core
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // 2. Transformar el recurso en un comando
        var command = CreateRecipeCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        // 3. Enviar el comando al servicio de comandos
        var recipe = await recipeCommandService.Handle(command);

        // 4. Transformar el aggregate de vuelta a un recurso para la respuesta
        // Nota: Para la creación, la receta aún no tendrá IngredientQuantity.
        // Por lo tanto, pasamos una lista vacía de ingredientes.
        var recipeResource = RecipeResourceFromEntityAssembler.ToResourceFromEntity(recipe, Enumerable.Empty<Ingredient>());

        // 5. Devolver una respuesta HTTP 201 Created
        return CreatedAtAction(nameof(GetRecipeById), new { recipeId = recipeResource.Id }, recipeResource);
    }
    
    // --- PUT /api/v1/Recipes/{recipeId}
    [HttpPut("{recipeId}")]
    [SwaggerOperation("Update an existing recipe.")]
    [SwaggerResponse(200, "Recipe updated successfully", typeof(RecipeResource))]
    [SwaggerResponse(400, "Invalid request payload")]
    [SwaggerResponse(404, "Recipe not found")]
    public async Task<IActionResult> UpdateRecipe([FromRoute] int recipeId, [FromBody] UpdateRecipeResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = UpdateRecipeCommandFromResourceAssembler.ToCommandFromResource(recipeId, resource);
        var updatedRecipe = await recipeCommandService.Handle(command);

        if (updatedRecipe is null)
        {
            return NotFound(); // La receta no fue encontrada para actualizar
        }

        // Para devolver el recurso actualizado, necesitamos los ingredientes.
        IEnumerable<Ingredient> allIngredientsInRecipe = new List<Ingredient>();
        if (updatedRecipe.Ingredients.Any())
        {
            var ingredientIds = updatedRecipe.Ingredients.Select(iq => iq.IngredientId).Distinct();
            allIngredientsInRecipe = await ingredientQueryService.Handle(new GetIngredientsByIdsQuery(ingredientIds));
        }

        var recipeResource = RecipeResourceFromEntityAssembler.ToResourceFromEntity(updatedRecipe, allIngredientsInRecipe);
        return Ok(recipeResource);
    }

    // --- DELETE /api/v1/Recipes/{recipeId}
    [HttpDelete("{recipeId}")]
    [SwaggerOperation("Delete a recipe by its ID.")]
    [SwaggerResponse(204, "Recipe deleted successfully")]
    [SwaggerResponse(404, "Recipe not found")]
    public async Task<IActionResult> DeleteRecipe([FromRoute] int recipeId)
    {
        var command = new DeleteRecipeCommand(recipeId); // No necesita assembler, el ID viene directo de la ruta
        var deletedRecipe = await recipeCommandService.Handle(command); // El CommandService devuelve el objeto borrado si existe

        if (deletedRecipe is null)
        {
            return NotFound(); // La receta no fue encontrada para borrar
        }

        return NoContent(); // 204 No Content para operaciones de borrado exitosas sin cuerpo de respuesta
    }

    // --- POST /api/v1/Recipes/{recipeId}/ingredients
    [HttpPost("{recipeId}/ingredients")]
    [SwaggerOperation("Add an ingredient to a recipe.")]
    [SwaggerResponse(200, "Ingredient added successfully", typeof(RecipeResource))]
    [SwaggerResponse(400, "Invalid request payload or ingredient already exists in recipe")]
    [SwaggerResponse(404, "Recipe or ingredient not found")]
    public async Task<IActionResult> AddIngredientToRecipe([FromRoute] int recipeId, [FromBody] AddIngredientToRecipeResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = AddIngredientToRecipeCommandFromResourceAssembler.ToCommandFromResource(recipeId, resource);
        var updatedRecipe = await recipeCommandService.Handle(command);

        if (updatedRecipe is null)
        {
            return NotFound(); // La receta no fue encontrada
        }
        // El CommandService podría lanzar una excepción si el ingrediente no existe,
        // o si ya fue añadido, lo cual se capturaría aquí y se devolvería un 400.
        // Para este ejemplo, si CommandService devuelve null, asumimos NotFound.
        // Si el CommandService lanza InvalidOperationException (ej. "Ingredient not found"),
        // podrías usar un middleware para mapear esa excepción a un 400/404.

        // Para devolver el recurso actualizado, necesitamos los ingredientes.
        IEnumerable<Ingredient> allIngredientsInRecipe = new List<Ingredient>();
        if (updatedRecipe.Ingredients.Any())
        {
            var ingredientIds = updatedRecipe.Ingredients.Select(iq => iq.IngredientId).Distinct();
            allIngredientsInRecipe = await ingredientQueryService.Handle(new GetIngredientsByIdsQuery(ingredientIds));
        }

        var recipeResource = RecipeResourceFromEntityAssembler.ToResourceFromEntity(updatedRecipe, allIngredientsInRecipe);
        return Ok(recipeResource);
    }

    // --- DELETE /api/v1/Recipes/{recipeId}/ingredients/{ingredientId}
    [HttpDelete("{recipeId}/ingredients/{ingredientId}")]
    [SwaggerOperation("Remove an ingredient from a recipe.")]
    [SwaggerResponse(200, "Ingredient removed successfully", typeof(RecipeResource))]
    [SwaggerResponse(404, "Recipe or ingredient not found in recipe")]
    public async Task<IActionResult> RemoveIngredientFromRecipe([FromRoute] int recipeId, [FromRoute] int ingredientId)
    {
        // En este caso, no hay un recurso de body, el comando se crea directamente con los IDs de la ruta.
        var command = new RemoveIngredientFromRecipeCommand(recipeId, ingredientId); 
        var updatedRecipe = await recipeCommandService.Handle(command);

        if (updatedRecipe is null)
        {
            return NotFound(); // La receta o el ingrediente en la receta no fue encontrado
        }

        // Para devolver el recurso actualizado, necesitamos los ingredientes.
        IEnumerable<Ingredient> allIngredientsInRecipe = new List<Ingredient>();
        if (updatedRecipe.Ingredients.Any())
        {
            var ingredientIds = updatedRecipe.Ingredients.Select(iq => iq.IngredientId).Distinct();
            allIngredientsInRecipe = await ingredientQueryService.Handle(new GetIngredientsByIdsQuery(ingredientIds));
        }
        var recipeResource = RecipeResourceFromEntityAssembler.ToResourceFromEntity(updatedRecipe, allIngredientsInRecipe);
        return Ok(recipeResource);
    }

    // --- PUT /api/v1/Recipes/{recipeId}/ingredients/{ingredientId}/quantity
    [HttpPut("{recipeId}/ingredients/{ingredientId}/quantity")]
    [SwaggerOperation("Update the quantity of an ingredient in a recipe.")]
    [SwaggerResponse(200, "Ingredient quantity updated successfully", typeof(RecipeResource))]
    [SwaggerResponse(400, "Invalid quantity provided")]
    [SwaggerResponse(404, "Recipe or ingredient not found in recipe")]
    public async Task<IActionResult> UpdateIngredientQuantity([FromRoute] int recipeId, [FromRoute] int ingredientId, [FromBody] UpdateIngredientQuantityResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // El comando toma el ingredientId de la ruta, y la nueva cantidad del body.
        var command = UpdateIngredientQuantityCommandFromResourceAssembler.ToCommandFromResource(recipeId, resource);
        var updatedRecipe = await recipeCommandService.Handle(command);

        if (updatedRecipe is null)
        {
            return NotFound(); // La receta o el ingrediente en la receta no fue encontrado
        }

        // Para devolver el recurso actualizado, necesitamos los ingredientes.
        IEnumerable<Ingredient> allIngredientsInRecipe = new List<Ingredient>();
        if (updatedRecipe.Ingredients.Any())
        {
            var ingredientIds = updatedRecipe.Ingredients.Select(iq => iq.IngredientId).Distinct();
            allIngredientsInRecipe = await ingredientQueryService.Handle(new GetIngredientsByIdsQuery(ingredientIds));
        }
        var recipeResource = RecipeResourceFromEntityAssembler.ToResourceFromEntity(updatedRecipe, allIngredientsInRecipe);
        return Ok(recipeResource);
    }
    
}