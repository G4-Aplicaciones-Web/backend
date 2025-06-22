using System.Net.Mime;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Queries;
using AlimentateplusPlatform.API.Tracking.Domain.Model.ValueObjects;
using AlimentateplusPlatform.API.Tracking.Domain.Services;
using AlimentateplusPlatform.API.Tracking.Interfaces.REST.Resources;
using AlimentateplusPlatform.API.Tracking.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AlimentateplusPlatform.API.Tracking.Interfaces.REST;

[ApiController]
[Route("api/v1/tracking")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Tracking Management Endpoints")]
public class TrackingController(
    ITrackingCommandService trackingCommandService,
    ITrackingQueryService trackingQueryService)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new tracking")]
    public async Task<IActionResult> CreateTracking([FromBody] CreateTrackingResource resource)
    {
        var command = CreateTrackingCommandFromResourceAssembler.ToCommand(resource);
        var tracking = await trackingCommandService.Handle(command);
        if (tracking is null) return BadRequest();

        var result = TrackingResourceFromEntityAssembler.ToResource(tracking);
        return CreatedAtAction(nameof(GetTrackingByUserId), new { userId = result.UserId }, result);
    }

    [HttpGet("user/{userId:long}")]
    [SwaggerOperation(Summary = "Get tracking by user ID")]
    public async Task<IActionResult> GetTrackingByUserId(int userId)
    {
        var query = new GetTrackingByUserIdQuery(new UserId(userId));
        var tracking = await trackingQueryService.Handle(query);
        return tracking is null ? NotFound() : Ok(TrackingResourceFromEntityAssembler.ToResource(tracking));
    }

    [HttpGet("macronutrients/consumed/tracking/{trackingId:long}")]
    [SwaggerOperation(Summary = "Get consumed macronutrients by tracking ID")]
    public async Task<IActionResult> GetConsumedMacros(int trackingId)
    {
        var query = new GetConsumedMacrosQuery(trackingId);
        var macros = await trackingQueryService.Handle(query);
        return macros is null
            ? NotFound()
            : Ok(TrackingMacronutrientResourceFromEntityAssembler.ToResource(macros));
    }
}