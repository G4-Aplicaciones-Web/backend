using System.Net.Mime;
using backendNetCore.MealPlans.Domain.Model.Queries;
using backendNetCore.MealPlans.Domain.Services;
using backendNetCore.MealPlans.Interfaces.REST.Resources;
using backendNetCore.MealPlans.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;


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
    public async Task<ActionResult> GetMealPlanById(int id)
    {
        var getMealPlanByIdQuery = new GetMealPlanByIdQuery(id);
        var result = await mealPlanQueryService.Handle(getMealPlanByIdQuery);
        if (result is null) return NotFound();
        var resource = MealPlanResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
}