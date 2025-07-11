using backendNetCore.Profiles.Domain.Model.Commands;
using backendNetCore.Profiles.Interfaces.REST.Resources;

namespace backendNetCore.Profiles.Interfaces.REST.Transform;

public static class CreateObjectiveCommandFromResourceAssembler
{
    public static CreateObjectiveCommand ToCommandFromResource(CreateObjectiveResource resource)
    {
        return new CreateObjectiveCommand(resource.Name, resource.Score);
    }
}