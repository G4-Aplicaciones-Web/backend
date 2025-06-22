using backendNetCore.Recommendations.Application.Internal.CommandServices;
using backendNetCore.Recommendations.Application.Internal.QueryServices;
using backendNetCore.Recommendations.Domain.Model.Commands;
using backendNetCore.Recommendations.Interfaces.REST.Resources;
using backendNetCore.Recommendations.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace backendNetCore.Recommendations.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/recommendations")]
public class RecommendationsController : ControllerBase
{
    private readonly IRecommendationCommandService _commandService;
    private readonly IRecommendationQueryService _queryService;

    public RecommendationsController(IRecommendationCommandService commandService, IRecommendationQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecommendationResource>>> GetAll()
    {
        var recommendations = await _queryService.GetAllAsync();
        var resources = RecommendationResourceAssembler.ToResourceListFromEntityList(recommendations);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RecommendationResource>> GetById(int id)
    {
        var recommendation = await _queryService.GetByIdAsync(id);
        if (recommendation == null) return NotFound();

        var resource = RecommendationResourceAssembler.ToResourceFromEntity(recommendation);
        return Ok(resource);
    }

    [HttpGet("user/{userId:long}")]
    public async Task<ActionResult<IEnumerable<RecommendationResource>>> GetByUserId(long userId)
    {
        var recommendations = await _queryService.GetByUserIdAsync(userId);
        var resources = RecommendationResourceAssembler.ToResourceListFromEntityList(recommendations);
        return Ok(resources);
    }

    [HttpPost]
    public async Task<ActionResult<RecommendationResource>> Create([FromBody] CreateRecommendationResource resource)
    {
        var command = CreateRecommendationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var recommendation = await _commandService.Handle(command);
        var resultResource = RecommendationResourceAssembler.ToResourceFromEntity(recommendation);

        return CreatedAtAction(nameof(GetById), new { id = resultResource.Id }, resultResource);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _commandService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
