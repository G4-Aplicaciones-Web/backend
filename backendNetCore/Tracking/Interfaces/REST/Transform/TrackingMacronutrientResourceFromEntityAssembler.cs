using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Interfaces.REST.Resources;

namespace backendNetCore.Tracking.Interfaces.REST.Transform;

public class TrackingMacronutrientResourceFromEntityAssembler
{
    public static TrackingMacronutrientResource ToResource(TrackingMacronutrient entity)
    {
        return new TrackingMacronutrientResource(
            entity.Id,
            entity.Calories,
            entity.Carbs,
            entity.Proteins,
            entity.Fats
        );
    }
}