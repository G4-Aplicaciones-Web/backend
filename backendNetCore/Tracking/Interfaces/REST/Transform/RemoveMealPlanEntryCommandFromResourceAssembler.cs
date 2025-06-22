using AlimentateplusPlatform.API.Tracking.Domain.Model.Commands;

namespace AlimentateplusPlatform.API.Tracking.Interfaces.REST.Transform;

public class RemoveMealPlanEntryCommandFromResourceAssembler
{
    public static RemoveMealPlanEntryFromTrackingCommand ToCommand(int trackingId, int mealPlanEntryId)
    {
        return new RemoveMealPlanEntryFromTrackingCommand(trackingId, mealPlanEntryId);
    }
}