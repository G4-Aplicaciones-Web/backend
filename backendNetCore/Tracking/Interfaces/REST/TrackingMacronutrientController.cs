using System.Net.Mime;
using backendNetCore.Tracking.Domain.Model.Queries;
using backendNetCore.Tracking.Domain.Services;
using backendNetCore.Tracking.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace backendNetCore.Tracking.Interfaces.REST;

[ApiController]
[Route("api/v1/macronutrients")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Macronutrient Values Management Endpoints")]
public class TrackingMacronutrientController(
    ITrackingQueryService trackingQueryService)
    : ControllerBase
{
    [HttpGet("consumed/tracking/{trackingId:long}")]
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
