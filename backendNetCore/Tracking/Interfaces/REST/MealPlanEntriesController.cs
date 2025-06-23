using System.Net.Mime;
using backendNetCore.Tracking.Domain.Model.Queries;
using backendNetCore.Tracking.Domain.Services;
using backendNetCore.Tracking.Interfaces.REST.Resources;
using backendNetCore.Tracking.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace backendNetCore.Tracking.Interfaces.REST;

[ApiController]
[Route("api/v1/meal-plan-entries")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Meal Plan Entries Management Endpoints")]
public class MealPlanEntriesController(
    ITrackingQueryService trackingQueryService,
    ITrackingCommandService trackingCommandService) : ControllerBase
{
    [HttpPost("{trackingId:long}")]
    [SwaggerOperation(Summary = "Add a meal plan entry to tracking", OperationId = "CreateMealPlanEntry")]
    [SwaggerResponse(StatusCodes.Status201Created, "Meal plan entry created")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
    public async Task<IActionResult> CreateMealPlanEntry([FromRoute] long trackingId, [FromBody] CreateMealPlanEntryResource resource)
    {
        try
        {
            var command = CreateMealPlanEntryCommandFromResourceAssembler.ToCommand(resource, trackingId);
            await trackingCommandService.Handle(command);
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { error = e.Message });
        }
    }

    [HttpGet("tracking/{trackingId:long}")]
    [SwaggerOperation(Summary = "Get all meals for a tracking", OperationId = "GetAllMeals")]
    [SwaggerResponse(StatusCodes.Status200OK, "Meals retrieved", typeof(IEnumerable<MealPlanEntriesResource>))]
    public async Task<IActionResult> GetAllMeals([FromRoute] int trackingId)
    {
        var query = new GetAllMealsQuery(trackingId);
        var meals = await trackingQueryService.Handle(query);
        var resources = meals.Select(MealPlanEntriesResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    [HttpPut("{mealPlanEntryId:long}")]
    [SwaggerOperation(Summary = "Update a meal plan entry", OperationId = "UpdateMealPlanEntry")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMealPlanEntry([FromRoute] int mealPlanEntryId, [FromBody] UpdateMealPlanEntryResource resource)
    {
        try
        {
            var command = UpdateMealPlanEntryCommandFromResourceAssembler.ToCommand(resource, mealPlanEntryId);
            var tracking = await trackingCommandService.Handle(command);
            if (tracking is null) return NotFound();
            return Ok();
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { error = e.Message });
        }
    }

    [HttpDelete("tracking/{trackingId:long}/entry/{mealPlanEntryId:long}")]
    [SwaggerOperation(Summary = "Remove a meal plan entry", OperationId = "RemoveMealPlanEntry")]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveMealPlanEntry([FromRoute] int trackingId, [FromRoute] int mealPlanEntryId)
    {
        try
        {
            var command = RemoveMealPlanEntryCommandFromResourceAssembler.ToCommand(trackingId, mealPlanEntryId);
            await trackingCommandService.Handle(command);
            return NoContent();
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { error = e.Message });
        }
    }
}