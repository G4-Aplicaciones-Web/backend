using System.Net.Mime;
using backendNetCore.Profiles.Domain.Model.Queries;
using backendNetCore.Profiles.Domain.Services;
using backendNetCore.Profiles.Interfaces.REST.Resources;
using backendNetCore.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace backendNetCore.Profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Objective Endpoints")]
public class ObjectivesController(
    IObjectiveCommandService objectiveCommandService,
    IObjectiveQueryService objectiveQueryService
    ) : ControllerBase
{
    [HttpGet("{objectiveId:int}")]
    [SwaggerOperation(
        Summary = "Get an objective by its ID",
        Description = "Get an objective by its ID",
        OperationId = "GetObjectiveById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of objectives", typeof(ObjectiveResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No objectives found")]
    public async Task<IActionResult> GetObjectiveById(int objectiveId)
    {
        var getObjectiveByIdQuery = new GetObjectiveByIdQuery(objectiveId);
        var objective = await objectiveQueryService.Handle(getObjectiveByIdQuery);
        if (objective is null) return NotFound();
        var resource = ObjectiveResourceFromEntityAssembler.ToResourceFromEntity(objective);
        return Ok(resource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new objective",
        Description = "Create a new objective",
        OperationId = "CreateObjective")]
    [SwaggerResponse(StatusCodes.Status201Created, "The objective was created", typeof(ObjectiveResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The objective could not be created")]
    public async Task<IActionResult> CreateObjective([FromBody] CreateObjectiveResource resource)
    {
        var createObjectiveCommand = CreateObjectiveCommandFromResourceAssembler.ToCommandFromResource(resource);
        var objective = await objectiveCommandService.Handle(createObjectiveCommand);
        if (objective is null) return BadRequest();
        var objectiveResource = ObjectiveResourceFromEntityAssembler.ToResourceFromEntity(objective);
        return CreatedAtAction(nameof(GetObjectiveById), new { objectiveId = objective.Id }, objectiveResource);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all objectives",
        Description = "Get all objectives",
        OperationId = "GetAllObjectives")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of objectives", typeof(IEnumerable<ObjectiveResource>))]
    public async Task<IActionResult> GetAllObjectives()
    {
        var objectives = await objectiveQueryService.Handle(new GetAllObjectivesQuery());
        var objectiveResources = objectives.Select(ObjectiveResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(objectiveResources);
    }
}