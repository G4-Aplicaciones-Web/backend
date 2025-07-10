using System.Net.Mime;
using backendNetCore.MealPlans.Domain.Model.Commands;
using backendNetCore.MealPlans.Domain.Model.Queries;
using backendNetCore.MealPlans.Domain.Services;
using backendNetCore.MealPlans.Interfaces.REST.Resources;
using backendNetCore.MealPlans.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace backendNetCore.MealPlans.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Meal plans")]
public class MealPlanController(
    IMealPlanCommandService mealPlanCommandService,
    IMealPlanQueryService mealPlanQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new meal plan",
        Description = "Creates a new meal plan in the system with the provided data",
        Tags = new[] { "Meal Plans" }
    )]
    [SwaggerResponse(201, "Meal plan created successfully", typeof(MealPlanResource))]
    [SwaggerResponse(400, "Invalid input data")]
    public async Task<ActionResult> CreateMealPlan([FromBody] CreateMealPlanResource resource)
    {
        var createMealPlanCommand = 
            CreateMealPlanCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await mealPlanCommandService.Handle(createMealPlanCommand);
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetMealPlanById), new { id = result.Id }, 
            MealPlanResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get meal plan by ID",
        Description = "Retrieves a specific meal plan using its unique identifier",
        Tags = new[] { "Meal Plans" }
    )]
    [SwaggerResponse(200, "Meal plan found", typeof(MealPlanResource))]
    [SwaggerResponse(404, "Meal plan not found")]
    public async Task<ActionResult> GetMealPlanById(int id)
    {
        var getMealPlanByIdQuery = new GetMealPlanByIdQuery(id);
        var result = await mealPlanQueryService.Handle(getMealPlanByIdQuery);
        if (result is null) return NotFound();
        var resource = MealPlanResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all meal plans",
        Description = "Retrieves all meal plans available in the system",
        Tags = new[] { "Meal Plans" }
    )]
    [SwaggerResponse(200, "Meal plans retrieved successfully", typeof(IEnumerable<MealPlanResource>))]
    public async Task<ActionResult> GetAllMealPlans()
    {
        var getAllMealPlansQuery = new GetAllMealPlansQuery();
        var result = await mealPlanQueryService.Handle(getAllMealPlansQuery);
        var resources = result.Select(MealPlanResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a meal plan",
        Description = "Updates an existing meal plan with the provided data",
        Tags = new[] { "Meal Plans" }
    )]
    [SwaggerResponse(200, "Meal plan updated successfully", typeof(MealPlanResource))]
    [SwaggerResponse(400, "Invalid input data")]
    [SwaggerResponse(404, "Meal plan not found")]
    public async Task<ActionResult> UpdateMealPlan(int id, [FromBody] UpdateMealPlanResource resource)
    {
        var updateMealPlanCommand = 
            UpdateMealPlanCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await mealPlanCommandService.Handle(updateMealPlanCommand);
        if (result is null) return NotFound();
        return Ok(MealPlanResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a meal plan",
        Description = "Deletes an existing meal plan from the system",
        Tags = new[] { "Meal Plans" }
    )]
    [SwaggerResponse(204, "Meal plan deleted successfully")]
    [SwaggerResponse(404, "Meal plan not found")]
    public async Task<ActionResult> DeleteMealPlan(int id)
    {
        var deleteMealPlanCommand = new DeleteMealPlanCommand(id);
        var result = await mealPlanCommandService.Handle(deleteMealPlanCommand);
        if (!result) return NotFound();
        return NoContent();
    }
}