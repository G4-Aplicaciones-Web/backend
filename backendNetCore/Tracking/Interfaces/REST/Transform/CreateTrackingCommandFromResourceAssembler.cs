using AlimentateplusPlatform.API.Tracking.Domain.Model.Commands;
using AlimentateplusPlatform.API.Tracking.Domain.Model.ValueObjects;
using AlimentateplusPlatform.API.Tracking.Interfaces.REST.Resources;

namespace AlimentateplusPlatform.API.Tracking.Interfaces.REST.Transform;

public class CreateTrackingCommandFromResourceAssembler
{
    public static CreateTrackingCommand ToCommand(CreateTrackingResource resource)
    {
        return new CreateTrackingCommand(new UserId(resource.UserId));
    }
}