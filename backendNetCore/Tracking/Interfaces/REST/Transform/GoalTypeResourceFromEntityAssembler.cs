using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Interfaces.REST.Resources;

namespace backendNetCore.Tracking.Interfaces.REST.Transform;

public class GoalTypeResourceFromEntityAssembler
{
    public static GoalTypeResource ToResourceFromEntity(GoalType goalType)
    {
        return new GoalTypeResource(
            goalType.DisplayName,
            goalType.DisplayName,
            goalType.Calories,
            goalType.Carbs,
            goalType.Proteins,
            goalType.Fats
        );
    }
    
    public static IEnumerable<GoalTypeResource> ToResourceFromEntity(IEnumerable<GoalType> goalTypes)
    {
        return goalTypes.Select(ToResourceFromEntity);
    }
}