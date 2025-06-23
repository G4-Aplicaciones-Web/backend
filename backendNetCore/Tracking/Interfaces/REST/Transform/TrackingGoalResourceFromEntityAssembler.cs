using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Interfaces.REST.Resources;

namespace backendNetCore.Tracking.Interfaces.REST.Transform;

public class TrackingGoalResourceFromEntityAssembler
{
    public static TrackingGoalResource ToResource(TrackingGoal goal)
    {
        var target = TrackingMacronutrientResourceFromEntityAssembler.ToResource(goal.TargetMacros);
        return new TrackingGoalResource(goal.Id, goal.UserId.Id, target);
    }
}