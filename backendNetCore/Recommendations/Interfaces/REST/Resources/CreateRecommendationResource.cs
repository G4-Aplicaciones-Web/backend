namespace backendNetCore.Recommendations.Interfaces.REST.Resources;

public class CreateRecommendationResource
{
    public long UserId { get; set; }
    public int TemplateId { get; set; }
    public string Reason { get; set; } = null!;
    public string Notes { get; set; } = null!;
    public string TimeOfDay { get; set; } = null!;
    public double Score { get; set; }
    public string Status { get; set; } = null!;
}