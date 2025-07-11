using backendNetCore.Recommendations.Domain.Model.Commands;
using backendNetCore.Recommendations.Interfaces.REST.Resources;

namespace backendNetCore.Recommendations.Interfaces.REST.Transform;

public static class AutoAssignRecommendationsCommandFromResourceAssembler
{
    public static AutoAssignRecommendationsCommand ToCommandFromResource(AutoAssignRecommendationsResource resource)
    {
        return new AutoAssignRecommendationsCommand(
            resource.UserId,
            resource.Count
        );
    }
}