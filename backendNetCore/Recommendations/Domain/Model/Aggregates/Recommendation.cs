
using backendNetCore.Recommendations.Domain.Model.Entities;
using backendNetCore.Recommendations.Domain.Model.ValueObjects;

namespace backendNetCore.Recommendations.Domain.Model.Aggregates;

public partial class Recommendation
{
    public int Id { get; private set; }

    // Asegúrate de que UserId NO tenga anotaciones de clave ni lo infiera EF como clave
    public UserId UserId { get; private set; }

    public int TemplateId { get; private set; }
    public RecommendationTemplate Template { get; private set; } = null!;
    public string Reason { get; private set; } = null!;
    public string Notes { get; private set; } = null!;
    public TimeOfDay TimeOfDay { get; private set; }
    public double Score { get; private set; }
    public RecommendationStatus Status { get; private set; }
    public DateTime AssignedAt { get; private set; }

    public Recommendation() => UserId = new UserId(0); // para EF Core

    public Recommendation(UserId userId, int templateId, string reason, string notes,
        TimeOfDay timeOfDay, double score, RecommendationStatus status)
    {
        UserId = userId;
        TemplateId = templateId;
        Reason = reason;
        Notes = notes;
        TimeOfDay = timeOfDay;
        Score = score;
        Status = status;
        AssignedAt = DateTime.UtcNow;
    }

    public static Recommendation AssignToUser(UserId userId, int templateId, string reason, string notes,
        TimeOfDay timeOfDay, double score, RecommendationStatus status)
    {
        return new Recommendation(userId, templateId, reason, notes, timeOfDay, score, status);
    }

    public static Recommendation CreateBase(int templateId, string reason, string notes,
        TimeOfDay timeOfDay, double score, RecommendationStatus status)
    {
        return new Recommendation(new UserId(0), templateId, reason, notes, timeOfDay, score, status);
    }

    public Recommendation CreateCopyForUser(UserId userId)
    {
        return new Recommendation(userId, TemplateId, Reason, Notes, TimeOfDay, Score, Status);
    }

    public void Update(string reason, string notes, TimeOfDay timeOfDay, double score, RecommendationStatus status)
    {
        Reason = reason;
        Notes = notes;
        TimeOfDay = timeOfDay;
        Score = score;
        Status = status;
    }

    public void Deactivate() => Status = RecommendationStatus.Inactive;
}
