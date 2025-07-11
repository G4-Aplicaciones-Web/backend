using backendNetCore.Recommendations.Domain.Model.Commands;
using backendNetCore.Recommendations.Domain.Model.ValueObjects;
using backendNetCore.Recommendations.Interfaces.REST.Resources;

namespace backendNetCore.Recommendations.Interfaces.REST.Transform;

public static class UpdateRecommendationCommandFromResourceAssembler
{
    public static UpdateRecommendationCommand ToCommandFromResource(int id, UpdateRecommendationResource resource)
    {
        var timeOfDay = Enum.Parse<TimeOfDay>(resource.TimeOfDay, true);
        var status = Enum.Parse<RecommendationStatus>(resource.Status, true);

        return new UpdateRecommendationCommand(
            id,
            resource.Reason,
            resource.Notes,
            timeOfDay,
            resource.Score,
            status
        );
    }
}