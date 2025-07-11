using backendNetCore.Profiles.Domain.Model.Commands;
using backendNetCore.Profiles.Interfaces.REST.Resources;

namespace backendNetCore.Profiles.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create a CreateProfileCommand command from a resource 
/// </summary>
public static class CreateProfileCommandFromResourceAssembler
{
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource)
    {
        return new CreateProfileCommand(
            resource.FirstName,
            resource.LastName,
            resource.Gender,
            resource.Height,
            resource.Weight,
            resource.Score,
            resource.ActivityLevelId,
            resource.ObjectiveId
        );
    }
}