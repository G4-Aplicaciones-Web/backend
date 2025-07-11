using System.Net.Mime;
using backendNetCore.Tracking.Domain.Model.Commands;
using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Model.Queries;
using backendNetCore.Tracking.Domain.Model.ValueObjects;
using backendNetCore.Tracking.Domain.Services;
using backendNetCore.Tracking.Interfaces.REST.Resources;
using backendNetCore.Tracking.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace backendNetCore.Tracking.Interfaces.REST;

[ApiController]
[Route("api/v1/tracking-goals")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Tracking Goals Management Endpoints")]
public class TrackingGoalController(
    ITrackingGoalCommandService trackingGoalCommandService,
    ITrackingGoalQueryService trackingGoalQueryService,
    ITrackingMacronutrientCommandService macronutrientCommandService,
    ITrackingMacronutrientQueryService macronutrientQueryService,
    IExternalProfileService externalProfileService)
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
    
    [HttpPost("by-objective")]
    [SwaggerOperation(Summary = "Create a new tracking goal based on objective type")]
    public async Task<IActionResult> CreateTrackingGoalByObjective([FromBody] CreateTrackingGoalByObjectiveResource resource)
    {
        try
        {
            var command = new CreateTrackingGoalByObjectiveCommand(new UserId(resource.UserId), resource.GoalType);
            var result = await trackingGoalCommandService.Handle(command);
            
            return CreatedAtAction(
                nameof(GetTrackingGoalByUserId), 
                new { userId = resource.UserId }, 
                TrackingGoalResourceFromEntityAssembler.ToResource(result));
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Invalid goal type: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error creating tracking goal: {ex.Message}");
        }
    }

    [HttpPut("{userId:int}")]
    [SwaggerOperation(Summary = "Update tracking goal for a user")]
    public async Task<IActionResult> UpdateTrackingGoal(int userId, [FromBody] UpdateTrackingGoalResource resource)
    {
        try
        {
            var goalType = GoalType.FromDisplayName(resource.GoalType);
            var command = new UpdateTrackingGoalCommand(new UserId(userId), goalType);
            
            var result = await trackingGoalCommandService.Handle(command);
            
            return Ok(TrackingGoalResourceFromEntityAssembler.ToResource(result));
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Invalid goal type: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error updating tracking goal: {ex.Message}");
        }
    }
    
    [HttpPost("from-profile/{profileId:int}")]
    [SwaggerOperation(Summary = "Create a tracking goal based on profile objective")]
    public async Task<IActionResult> CreateTrackingGoalFromProfile(int profileId)
    {
        try
        {
            var trackingGoal = await externalProfileService.CreateTrackingGoalBasedOnProfile(profileId);
            
            return CreatedAtAction(
                nameof(GetTrackingGoalByUserId), 
                new { userId = profileId }, 
                TrackingGoalResourceFromEntityAssembler.ToResource(trackingGoal));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating the tracking goal", details = ex.Message });
        }
    }
}
