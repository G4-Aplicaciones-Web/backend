using backendNetCore.Recommendations.Domain.Model.Aggregates;
using backendNetCore.Recommendations.Domain.Model.Commands;
using backendNetCore.Recommendations.Domain.Model.ValueObjects;
using backendNetCore.Recommendations.Domain.Model.Repositories;
using backendNetCore.Recommendations.Application.Internal.OutBoundServices.ACL;

namespace backendNetCore.Recommendations.Application.Internal.CommandServices;

public class RecommendationCommandService : IRecommendationCommandService
{
    private readonly IRecommendationRepository _recommendationRepository;
    private readonly ExternalProfileService _externalProfileService;
    private readonly ExternalIamService _externalIamService;

    public RecommendationCommandService(
        IRecommendationRepository recommendationRepository,
        ExternalProfileService externalProfileService,
        ExternalIamService externalIamService)
    {
        _recommendationRepository = recommendationRepository;
        _externalProfileService = externalProfileService;
        _externalIamService = externalIamService;
    }

    public async Task<Recommendation> Handle(AssignRecommendationCommand command)
    {
        if (!await _externalProfileService.ExistsUserById(command.UserId) ||
            !await _externalIamService.ExistsUserById(command.UserId))
            throw new InvalidOperationException($"El usuario con ID {command.UserId} no existe en IAM.");

        var userId = new UserId(command.UserId);
        var recommendation = Recommendation.AssignToUser(
            userId,
            command.TemplateId,
            command.Reason,
            command.Notes,
            command.TimeOfDay,
            command.Score,
            command.Status
        );

        await _recommendationRepository.AddAsync(recommendation);
        return recommendation;
    }

    public async Task<Recommendation> Handle(UpdateRecommendationCommand command)
    {
        var recommendation = await _recommendationRepository.GetByIdAsync(command.Id);

        if (recommendation == null)
            throw new ArgumentException("Recommendation not found");

        if (!await _externalIamService.ExistsUserById(recommendation.UserId.Value))
            throw new InvalidOperationException($"El usuario con ID {recommendation.UserId.Value} no existe en IAM.");

        recommendation.Update(
            command.Reason,
            command.Notes,
            command.TimeOfDay,
            command.Score,
            command.Status
        );

        await _recommendationRepository.UpdateAsync(recommendation);
        return recommendation;
    }

    public async Task<Recommendation> Handle(CreateBaseRecommendationCommand command)
    {
        var recommendation = Recommendation.CreateBase(
            command.TemplateId,
            command.Reason,
            command.Notes,
            command.TimeOfDay,
            command.Score,
            command.Status
        );

        await _recommendationRepository.AddAsync(recommendation);
        return recommendation;
    }

    public async Task<IEnumerable<Recommendation>> Handle(AutoAssignRecommendationsCommand command)
    {
        if (!await _externalProfileService.ExistsUserById(command.UserId) ||
            !await _externalIamService.ExistsUserById(command.UserId))
            throw new InvalidOperationException($"El usuario con ID {command.UserId} no existe en IAM.");

        var baseRecommendations = await _recommendationRepository.GetBaseRecommendationsAsync();
        var baseRecommendationsList = baseRecommendations.ToList();

        if (!baseRecommendationsList.Any())
            throw new InvalidOperationException("No base recommendations available");

        var random = new Random();
        var selectedRecommendations = baseRecommendationsList
            .OrderBy(x => random.Next())
            .Take(command.Count)
            .ToList();

        var assignedRecommendations = new List<Recommendation>();
        var userId = new UserId(command.UserId);

        foreach (var baseRecommendation in selectedRecommendations)
        {
            var assignedRecommendation = baseRecommendation.CreateCopyForUser(userId);
            await _recommendationRepository.AddAsync(assignedRecommendation);
            assignedRecommendations.Add(assignedRecommendation);
        }

        return assignedRecommendations;
    }

    public async Task Handle(DeleteRecommendationCommand command)
    {
        var recommendation = await _recommendationRepository.GetByIdAsync(command.RecommendationId);

        if (recommendation == null)
            throw new ArgumentException("Recommendation not found");

        await _recommendationRepository.DeleteAsync(recommendation);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var recommendation = await _recommendationRepository.GetByIdAsync(id);
        if (recommendation == null) return false;

        await _recommendationRepository.DeleteAsync(recommendation);
        return true;
    }
}
