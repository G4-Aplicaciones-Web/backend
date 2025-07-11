using backendNetCore.Profiles.Domain.Model.Commands;
using backendNetCore.Profiles.Interfaces.REST.Resources;

namespace backendNetCore.Profiles.Interfaces.REST.Transform;

public static class UpdateProfileCommandFromResourceAssembler
{
    public static UpdateProfileCommand ToCommandFromResource(int profileId, UpdateProfileResource resource )
    {
        return new UpdateProfileCommand(profileId, resource.Height, resource.Weight, resource.ObjectiveId, resource.ActivityLevelId);
    }
}