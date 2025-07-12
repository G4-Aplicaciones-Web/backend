using backendNetCore.IAM.Interfaces.ACL;
using backendNetCore.Profiles.Interfaces.ACL;

namespace backendNetCore.Recommendations.Application.Internal.OutBoundServices.ACL;

/// <summary>
/// Servicio externo para validar usuarios y perfiles desde Recommendations
/// ACL entre Recommendations, Profiles e IAM
/// </summary>
public class ExternalProfileService
{
    private readonly IProfilesContextFacade _profilesContextFacade;
    private readonly IIamContextFacade _iamContextFacade;

    public ExternalProfileService(
        IProfilesContextFacade profilesContextFacade,
        IIamContextFacade iamContextFacade)
    {
        _profilesContextFacade = profilesContextFacade;
        _iamContextFacade = iamContextFacade;
    }

    public async Task<bool> ExistsUserById(long userId)
    {
        var username = await _iamContextFacade.FetchUsernameByUserId((int)userId);
        return !string.IsNullOrEmpty(username);
    }

    public async Task<bool> ExistsProfileById(int profileId)
    {
        return await _profilesContextFacade.ExistsProfileById(profileId);
    }
}