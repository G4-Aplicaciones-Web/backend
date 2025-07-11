using backendNetCore.Profiles.Domain.Model.Commands;
using backendNetCore.Profiles.Interfaces.REST.Resources;

namespace backendNetCore.Profiles.Interfaces.REST.Transform;

public static class AddAllergyToProfileCommandFromResourceAssembler
{
    public static AddAllergyToProfileCommand ToCommandFromResource(int profileId, AddAllergyToProfileResource resource)
    {
        return new AddAllergyToProfileCommand(profileId, resource.AllergyName);
    }
}