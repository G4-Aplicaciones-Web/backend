using System.Net.Mime;
using backendNetCore.MealPlans.Domain.Model.Queries;
using backendNetCore.MealPlans.Domain.Services;
using backendNetCore.MealPlans.Interfaces.REST.Resources;
using backendNetCore.MealPlans.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace backendNetCore.MealPlans.Interfaces.REST;

[ApiController]
[Route("api/v1/mealplan")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Meal plans")]
public class MealPlanController(
    IMealPlanCommandService mealPlanCommandService,
    IMealPlanQueryService mealPlanQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new meal plan",
        Description = "Creates a new meal plan with the provided information")]
    [SwaggerResponse(201, "Meal plan created successfully", typeof(MealPlanResource))]
    [SwaggerResponse(400, "Invalid request data")]
    public async Task<ActionResult> CreateMealPlan([FromBody] CreateMealPlanResource resource)
    {
        try
        {
            var createMealPlanCommand = 
                CreateMealPlanCommandFromResourceAssembler.ToCommandFromResource(resource);
            var result = await mealPlanCommandService.Handle(createMealPlanCommand);
            if (result is null) return BadRequest("Failed to create meal plan");
            return CreatedAtAction(nameof(GetMealPlanById), new { id = result.Id }, 
                MealPlanResourceFromEntityAssembler.ToResourceFromEntity(result));
        }
        catch (Exception ex) when (ex.Message == "Meal plan already exists")
        {
            return Conflict("A meal plan with this ProfileId and Score already exists");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get meal plan by ID",
        Description = "Retrieves a specific meal plan by its unique identifier")]
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
}