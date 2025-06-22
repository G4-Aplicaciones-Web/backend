using System.Net.Mime;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Queries;
using AlimentateplusPlatform.API.Tracking.Domain.Services;
using AlimentateplusPlatform.API.Tracking.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AlimentateplusPlatform.API.Tracking.Interfaces.REST;

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
