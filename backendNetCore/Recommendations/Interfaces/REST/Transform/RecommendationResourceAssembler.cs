using backendNetCore.Recommendations.Domain.Model.Aggregates;
using backendNetCore.Recommendations.Interfaces.REST.Resources;

namespace backendNetCore.Recommendations.Interfaces.REST.Transform;

public static class RecommendationResourceAssembler
{
    public static RecommendationResource ToResourceFromEntity(Recommendation recommendation)
    {
        return new RecommendationResource
        {
            Id = recommendation.Id,
            UserId = recommendation.UserId.Value,
            TemplateId = recommendation.TemplateId,
            Reason = recommendation.Reason,
            Notes = recommendation.Notes,
            TimeOfDay = recommendation.TimeOfDay.ToString(),
            Score = (decimal)recommendation.Score,
            Status = recommendation.Status.ToString(),
            AssignedAt = recommendation.AssignedAt,
            CreatedDate = recommendation.CreatedDate.GetValueOrDefault().UtcDateTime,
            UpdatedDate = recommendation.UpdatedDate.GetValueOrDefault().UtcDateTime
        };
    }

    public static IEnumerable<RecommendationResource> ToResourceListFromEntityList(IEnumerable<Recommendation> recommendations)
    {
        return recommendations.Select(ToResourceFromEntity);
    }
}