using System.Net.Mime;
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
[SwaggerTag("Ingredients Management Endpoints")]
public class IngredientsController(
    IIngredientCommandService ingredientCommandService,
    IIngredientQueryService ingredientQueryService
    ) : ControllerBase
{
    // --- POST /api/v1/Ingredients
    [HttpPost]
    [SwaggerOperation("Create a new ingredient.")]
    [SwaggerResponse(201, "Ingredient created successfully", typeof(IngredientResource))]
    [SwaggerResponse(400, "Invalid request payload")]
    public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredientResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = CreateIngredientCommandFromResourceAssembler.ToCommandFromResource(resource);
        var ingredient = await ingredientCommandService.Handle(command);

        if (ingredient is null)
        {
            // Esto podría ocurrir si hay alguna validación más profunda en el servicio de comando
            // que impida la creación (ej. nombre duplicado, si aplicara).
            return BadRequest("Unable to create ingredient. Check input data.");
        }

        var ingredientResource = IngredientResourceFromEntityAssembler.ToResourceFromEntity(ingredient);
        return CreatedAtAction(nameof(GetIngredientById), new { ingredientId = ingredientResource.Id }, ingredientResource);
    }

    // --- GET /api/v1/Ingredients/{ingredientId}
    [HttpGet("{ingredientId}")]
    [SwaggerOperation("Get an ingredient by its ID.")]
    [SwaggerResponse(200, "Ingredient found", typeof(IngredientResource))]
    [SwaggerResponse(404, "Ingredient not found")]
    public async Task<IActionResult> GetIngredientById([FromRoute] int ingredientId)
    {
        var ingredient = await ingredientQueryService.Handle(new GetIngredientByIdQuery(ingredientId));
        if (ingredient is null) return NotFound();

        var ingredientResource = IngredientResourceFromEntityAssembler.ToResourceFromEntity(ingredient);
        return Ok(ingredientResource);
    }

    // --- GET /api/v1/Ingredients/by-name?name={name}
    [HttpGet("by-name")]
    [SwaggerOperation("Get an ingredient by its name.")]
    [SwaggerResponse(200, "Ingredient found", typeof(IngredientResource))]
    [SwaggerResponse(404, "Ingredient not found")]
    public async Task<IActionResult> GetIngredientByName([FromQuery] string name)
    {
        var ingredient = await ingredientQueryService.Handle(new GetIngredientByNameQuery(name));
        if (ingredient is null) return NotFound();

        var ingredientResource = IngredientResourceFromEntityAssembler.ToResourceFromEntity(ingredient);
        return Ok(ingredientResource);
    }

    // --- GET /api/v1/Ingredients
    [HttpGet]
    [SwaggerOperation("Get all ingredients.")]
    [SwaggerResponse(200, "List of ingredients", typeof(IEnumerable<IngredientResource>))]
    public async Task<IActionResult> GetAllIngredients()
    {
        var ingredients = await ingredientQueryService.Handle(new GetAllIngredientsQuery());
        var ingredientResources = ingredients.Select(IngredientResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(ingredientResources);
    }

    // --- GET /api/v1/Ingredients/by-category/{category}
    [HttpGet("by-category/{category}")]
    [SwaggerOperation("Get ingredients by category.")]
    [SwaggerResponse(200, "Matching ingredients by category", typeof(IEnumerable<IngredientResource>))]
    public async Task<IActionResult> GetIngredientsByCategory([FromRoute] ECategory category)
    {
        var ingredients = await ingredientQueryService.Handle(new GetIngredientsByCategoryQuery(category));
        var ingredientResources = ingredients.Select(IngredientResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(ingredientResources);
    }

    // --- PUT /api/v1/Ingredients/{ingredientId}
    [HttpPut("{ingredientId}")]
    [SwaggerOperation("Update an existing ingredient.")]
    [SwaggerResponse(200, "Ingredient updated successfully", typeof(IngredientResource))]
    [SwaggerResponse(400, "Invalid request payload")]
    [SwaggerResponse(404, "Ingredient not found")]
    public async Task<IActionResult> UpdateIngredient([FromRoute] int ingredientId, [FromBody] UpdateIngredientResource resource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = UpdateIngredientCommandFromResourceAssembler.ToCommandFromResource(ingredientId, resource);
        var updatedIngredient = await ingredientCommandService.Handle(command);

        if (updatedIngredient is null)
        {
            return NotFound(); // El ingrediente no fue encontrado para actualizar
        }

        var ingredientResource = IngredientResourceFromEntityAssembler.ToResourceFromEntity(updatedIngredient);
        return Ok(ingredientResource);
    }

    // --- GET /api/v1/Ingredients/by-ids?ids=1&ids=2&ids=3 (ejemplo de cómo se usaría)
    [HttpGet("by-ids")]
    [SwaggerOperation("Get ingredients by a list of IDs.")]
    [SwaggerResponse(200, "Matching ingredients by IDs", typeof(IEnumerable<IngredientResource>))]
    public async Task<IActionResult> GetIngredientsByIds([FromQuery] IEnumerable<int> ids)
    {
        var ingredients = await ingredientQueryService.Handle(new GetIngredientsByIdsQuery(ids));
        var ingredientResources = ingredients.Select(IngredientResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(ingredientResources);
    }
}