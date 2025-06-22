using backendNetCore.Recommendations.Domain.Model.Entities;
using backendNetCore.Recommendations.Infrastructure.Persistence.EFC.repositories;
using backendNetCore.Recommendations.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc;

namespace backendNetCore.Recommendations.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/recommendation-templates")]
public class RecommendationTemplatesController : ControllerBase
{
    private readonly RecommendationTemplateRepository _repository;

    public RecommendationTemplatesController(RecommendationTemplateRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<RecommendationTemplate>> GetAll()
    {
        return await _repository.GetAllAsync();
    }

    [HttpPost]
    public async Task<ActionResult<RecommendationTemplate>> Create([FromBody] CreateTemplateResource resource)
    {
        var template = new RecommendationTemplate(resource.Title, resource.Content);
        await _repository.AddAsync(template);
        return CreatedAtAction(nameof(GetAll), new { id = template.Id }, template);
    }
    
}