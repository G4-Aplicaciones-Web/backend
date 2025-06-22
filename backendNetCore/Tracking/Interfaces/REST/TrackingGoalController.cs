using System.Net.Mime;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Commands;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Queries;
using AlimentateplusPlatform.API.Tracking.Domain.Model.ValueObjects;
using AlimentateplusPlatform.API.Tracking.Domain.Services;
using AlimentateplusPlatform.API.Tracking.Interfaces.REST.Resources;
using AlimentateplusPlatform.API.Tracking.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AlimentateplusPlatform.API.Tracking.Interfaces.REST;

[ApiController]
[Route("api/v1/tracking-goals")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Tracking Goals Management Endpoints")]
public class TrackingGoalController(
    ITrackingGoalCommandService trackingGoalCommandService,
    ITrackingGoalQueryService trackingGoalQueryService,
    ITrackingMacronutrientCommandService macronutrientCommandService,
    ITrackingMacronutrientQueryService macronutrientQueryService)
    : ControllerBase
{
    [HttpGet("user/{userId:long}")]
    [SwaggerOperation(Summary = "Get tracking goal by user ID")]
    public async Task<IActionResult> GetTrackingGoalByUserId(int userId)
    {
        var query = new GetTrackingGoalByUserIdQuery(new UserId(userId));
        var goal = await trackingGoalQueryService.Handle(query);
        return goal is null ? NotFound() : Ok(TrackingGoalResourceFromEntityAssembler.ToResource(goal));
    }

    [HttpGet("{trackingGoalId:long}/target-macros")]
    [SwaggerOperation(Summary = "Get target macronutrients by tracking goal ID")]
    public async Task<IActionResult> GetTargetMacros(int trackingGoalId)
    {
        var query = new GetTargetMacronutrientsQuery(trackingGoalId);
        var macros = await trackingGoalQueryService.Handle(query);
        return macros is null
            ? NotFound()
            : Ok(TrackingMacronutrientResourceFromEntityAssembler.ToResource(macros));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new tracking goal")]
    public async Task<IActionResult> CreateTrackingGoal([FromBody] CreateTrackingGoalResource resource)
    {
        var macroCommand = new CreateMacronutrientValuesCommand(
            0,
            resource.TargetMacros.Calories,
            resource.TargetMacros.Carbs,
            resource.TargetMacros.Proteins,
            resource.TargetMacros.Fats);

        var macro = await macronutrientCommandService.Handle(macroCommand);
        if (macro is null) return BadRequest();

        var command = new CreateTrackingGoalCommand(new UserId(resource.UserId), macro);
        var goalId = await trackingGoalCommandService.Handle(command);

        return CreatedAtAction(nameof(GetTrackingGoalByUserId), new { userId = resource.UserId }, goalId);
    }
}
