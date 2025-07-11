using backendNetCore.Profiles.Domain.Model.Entities;
using backendNetCore.Profiles.Interfaces.REST.Resources;

namespace backendNetCore.Profiles.Interfaces.REST.Transform;

public static class ObjectiveResourceFromEntityAssembler
{
    public static ObjectiveResource ToResourceFromEntity(Objective entity)
    {
        return new ObjectiveResource(entity.Id, entity.Name, entity.Score);
    }
}