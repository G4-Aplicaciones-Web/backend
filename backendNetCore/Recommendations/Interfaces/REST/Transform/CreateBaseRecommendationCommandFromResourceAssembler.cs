using backendNetCore.Recommendations.Domain.Model.Commands;
using backendNetCore.Recommendations.Domain.Model.ValueObjects;
using backendNetCore.Recommendations.Interfaces.REST.Resources;

namespace backendNetCore.Recommendations.Interfaces.REST.Transform;

public static class CreateBaseRecommendationCommandFromResourceAssembler
{
    public static CreateBaseRecommendationCommand ToCommandFromResource(CreateBaseRecommendationResource resource)
    {
        var timeOfDay = Enum.Parse<TimeOfDay>(resource.TimeOfDay, true);
        var status = Enum.Parse<RecommendationStatus>(resource.Status, true);

        return new CreateBaseRecommendationCommand(
            resource.TemplateId,
            resource.Reason,
            resource.Notes,
            timeOfDay,
            resource.Score,
            status
        );
    }
}