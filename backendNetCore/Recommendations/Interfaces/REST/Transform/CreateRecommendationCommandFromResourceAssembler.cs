using backendNetCore.Recommendations.Domain.Model.Commands;
using backendNetCore.Recommendations.Domain.Model.ValueObjects;
using backendNetCore.Recommendations.Interfaces.REST.Resources;

namespace backendNetCore.Recommendations.Interfaces.REST.Transform;

public static class CreateRecommendationCommandFromResourceAssembler
{
    public static AssignRecommendationCommand ToCommandFromResource(CreateRecommendationResource resource)
    {
        var timeOfDay = Enum.Parse<TimeOfDay>(resource.TimeOfDay, true);
        var status = Enum.Parse<RecommendationStatus>(resource.Status, true);

        return new AssignRecommendationCommand(
            resource.UserId,
            resource.TemplateId,
            resource.Reason,
            resource.Notes,
            timeOfDay,
            resource.Score,
            status
        );
    }
}