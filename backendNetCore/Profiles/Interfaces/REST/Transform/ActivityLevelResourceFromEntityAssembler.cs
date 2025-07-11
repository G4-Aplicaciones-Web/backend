using backendNetCore.Profiles.Domain.Model.Entities;
using backendNetCore.Profiles.Interfaces.REST.Resources;

namespace backendNetCore.Profiles.Interfaces.REST.Transform;

public static class ActivityLevelResourceFromEntityAssembler
{
    public static ActivityLevelResource ToResourceFromEntity(ActivityLevel entity)
    {
        return new ActivityLevelResource(entity.Id, entity.Name, entity.Description, entity.ActivityFactor);
    }
}