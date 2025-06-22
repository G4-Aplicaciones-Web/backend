namespace backendNetCore.Recommendations.Interfaces.REST.Resources;

public class RecommendationResource
{
    public int Id { get; set; }
    public long UserId { get; set; }
    public int TemplateId { get; set; }
    public string Reason { get; set; }
    public string Notes { get; set; }
    public string TimeOfDay { get; set; }
    public decimal Score { get; set; }
    public string Status { get; set; }
    public DateTime AssignedAt { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}