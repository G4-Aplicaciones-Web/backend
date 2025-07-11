namespace backendNetCore.Recommendations.Interfaces.REST.Resources;

public class AutoAssignRecommendationsResource
{
    public long UserId { get; set; }
    public int Count { get; set; } = 4;
}