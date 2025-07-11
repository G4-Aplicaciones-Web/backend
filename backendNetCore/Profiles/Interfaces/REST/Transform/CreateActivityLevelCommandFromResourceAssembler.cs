using backendNetCore.Profiles.Domain.Model.Commands;
using backendNetCore.Profiles.Interfaces.REST.Resources;

namespace backendNetCore.Profiles.Interfaces.REST.Transform;

public static class CreateActivityLevelCommandFromResourceAssembler
{
    public static CreateActivityLevelCommand ToCommandFromResource(CreateActivityLevelResource resource)
    {
        return new CreateActivityLevelCommand(resource.Name, resource.Description, resource.ActivityFactor);
    }
}