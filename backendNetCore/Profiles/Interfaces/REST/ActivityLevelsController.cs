using System.Net.Mime;
using backendNetCore.Profiles.Domain.Model.Queries;
using backendNetCore.Profiles.Domain.Services;
using backendNetCore.Profiles.Interfaces.REST.Resources;
using backendNetCore.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace backendNetCore.Profiles.Interfaces.REST;

/// <summary>
///     The Activity Levels controller
/// </summary>
/// <param name="activityLevelCommandService">
///     The <see cref="IActivityLevelCommandService" /> activity level command service
/// </param>
/// <param name="activityLevelQueryService">
///     The <see cref="IActivityLevelQueryService" /> activity level query service
/// </param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Activity Level Endpoints")]
public class ActivityLevelsController(
    IActivityLevelCommandService activityLevelCommandService,
    IActivityLevelQueryService activityLevelQueryService) : ControllerBase
{
    /// <summary>
    ///     Get activity level by ID
    /// </summary>
    /// <param name="activityLevelId">
    ///     The activity level ID
    /// </param>
    /// <returns>
    ///     The <see cref="ActivityLevelResource" /> activity level
    /// </returns>
    [HttpGet("{activityLevelId:int}")]
    [SwaggerOperation(
        Summary = "Get an activity level by its ID",
        Description = "Get an activity level by its ID",
        OperationId = "GetActivityLevelById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The activity level", typeof(ActivityLevelResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No activity level found")]
    public async Task<IActionResult> GetActivityLevelById(int activityLevelId)
    {
        var getActivityLevelByIdQuery = new GetActivityLevelByIdQuery(activityLevelId);
        var activityLevel = await activityLevelQueryService.Handle(getActivityLevelByIdQuery);
        if (activityLevel is null) return NotFound();
        var resource = ActivityLevelResourceFromEntityAssembler.ToResourceFromEntity(activityLevel);
        return Ok(resource);
    }

    /// <summary>
    ///     Create a new activity level
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateActivityLevelResource" /> activity level resource
    /// </param>
    /// <returns>
    ///     The <see cref="ActivityLevelResource" /> activity level created, or a bad request if the activity level could not be created
    /// </returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new activity level",
        Description = "Create a new activity level",
        OperationId = "CreateActivityLevel")]
    [SwaggerResponse(StatusCodes.Status201Created, "The activity level was created", typeof(ActivityLevelResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The activity level could not be created")]
    public async Task<IActionResult> CreateActivityLevel([FromBody] CreateActivityLevelResource resource)
    {
        var createActivityLevelCommand = CreateActivityLevelCommandFromResourceAssembler.ToCommandFromResource(resource);
        var activityLevel = await activityLevelCommandService.Handle(createActivityLevelCommand);
        if (activityLevel is null) return BadRequest();
        var activityLevelResource = ActivityLevelResourceFromEntityAssembler.ToResourceFromEntity(activityLevel);
        return CreatedAtAction(nameof(GetActivityLevelById), new { activityLevelId = activityLevel.Id }, activityLevelResource);
    }

    /// <summary>
    ///     Get all activity levels
    /// </summary>
    /// <returns>
    ///     The list of <see cref="ActivityLevelResource" /> activity levels
    /// </returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all activity levels",
        Description = "Get all activity levels",
        OperationId = "GetAllActivityLevels")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of activity levels", typeof(IEnumerable<ActivityLevelResource>))]
    public async Task<IActionResult> GetAllActivityLevels()
    {
        var activityLevels = await activityLevelQueryService.Handle(new GetAllActivityLevelsQuery());
        var activityLevelResources = activityLevels.Select(ActivityLevelResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(activityLevelResources);
    }
}