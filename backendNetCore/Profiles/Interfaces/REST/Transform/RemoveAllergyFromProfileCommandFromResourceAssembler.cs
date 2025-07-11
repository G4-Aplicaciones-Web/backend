using backendNetCore.Profiles.Domain.Model.Commands;
using backendNetCore.Profiles.Interfaces.REST.Resources;

namespace backendNetCore.Profiles.Interfaces.REST.Transform;

public static class RemoveAllergyFromProfileCommandFromResourceAssembler
{
    public static RemoveAllergyFromProfileCommand ToCommandFromResource(int profileId, RemoveAllergyFromProfileResource resource)
    {
        return new RemoveAllergyFromProfileCommand(profileId, resource.AllergyName);
    }
}