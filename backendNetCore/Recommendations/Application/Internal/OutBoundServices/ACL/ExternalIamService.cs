// backendNetCore/Recommendations/Application/Internal/OutBoundServices/ACL/ExternalIamService.cs
using backendNetCore.IAM.Interfaces.ACL;

namespace backendNetCore.Recommendations.Application.Internal.OutBoundServices.ACL;

public class ExternalIamService
{
    private readonly IIamContextFacade _iamContextFacade;

    public ExternalIamService(IIamContextFacade iamContextFacade)
    {
        _iamContextFacade = iamContextFacade;
    }

    public async Task<bool> ExistsUserById(long userId)
    {
        var username = await _iamContextFacade.FetchUsernameByUserId((int)userId);
        return !string.IsNullOrEmpty(username);
    }

    public async Task<bool> ExistsUserByUsername(string username)
    {
        var id = await _iamContextFacade.FetchUserIdByUsername(username);
        return id > 0;
    }
}