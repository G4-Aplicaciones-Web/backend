using backendNetCore.Tracking.Domain.Model.Commands;
using backendNetCore.Tracking.Domain.Model.ValueObjects;
using backendNetCore.Tracking.Interfaces.REST.Resources;

namespace backendNetCore.Tracking.Interfaces.REST.Transform;

public class CreateTrackingCommandFromResourceAssembler
{
    public static CreateTrackingCommand ToCommand(CreateTrackingResource resource)
    {
        return new CreateTrackingCommand(new UserId(resource.UserId));
    }
}