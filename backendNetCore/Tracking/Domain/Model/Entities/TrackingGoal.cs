using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Model.ValueObjects;

namespace backendNetCore.Tracking.Domain.Model.Entities;

// TrackingGoal Entity
public class TrackingGoal
{
    public int Id { get; set; }
    public UserId UserId { get; set; }
    public TrackingMacronutrient TargetMacros { get; set; }

    // Constructor requerido por EF Core
    public TrackingGoal()
    {
    }

    public TrackingGoal(UserId userId, TrackingMacronutrient targetMacros)
    {
        UserId = userId;
        TargetMacros = targetMacros;
    }
}