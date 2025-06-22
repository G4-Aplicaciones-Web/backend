using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;
using AlimentateplusPlatform.API.Tracking.Interfaces.REST.Resources;

namespace AlimentateplusPlatform.API.Tracking.Interfaces.REST.Transform;

public class TrackingGoalResourceFromEntityAssembler
{
    public static TrackingGoalResource ToResource(TrackingGoal goal)
    {
        var target = TrackingMacronutrientResourceFromEntityAssembler.ToResource(goal.TargetMacros);
        return new TrackingGoalResource(goal.Id, goal.UserId.Id, target);
    }
}