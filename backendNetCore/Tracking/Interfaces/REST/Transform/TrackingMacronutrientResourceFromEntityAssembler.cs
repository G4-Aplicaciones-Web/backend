using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;
using AlimentateplusPlatform.API.Tracking.Interfaces.REST.Resources;

namespace AlimentateplusPlatform.API.Tracking.Interfaces.REST.Transform;

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