using backendNetCore.IAM.Domain.Model.Commands;
using backendNetCore.IAM.Domain.Model.Queries;
using backendNetCore.IAM.Domain.Services;
using backendNetCore.IAM.Interfaces.ACL;

namespace backendNetCore.IAM.Application.ACL.Services;

/// <summary>
/// IAM Context Facade 
/// </summary>
/// <remarks>
/// This facade is responsible for handling the context of the IAM module.
/// </remarks>
/// <param name="userCommandService">
/// <see cref="IUserCommandService"/> User command service
/// </param>
/// <param name="userQueryService">
/// <see cref="IUserQueryService"/> User query service
/// </param>
public class IamContextFacade(
    IUserCommandService userCommandService,
    IUserQueryService userQueryService) : IIamContextFacade
{
    // <inheritdoc />
    public async Task<int> CreateUser(string username, string password)
    {
        var signUpCommand = new SignUpCommand(username, password);
        await userCommandService.Handle(signUpCommand);
        var getUserByUsernameQuery = new GetUserByUsernameQuery(username);
        var result = await userQueryService.Handle(getUserByUsernameQuery);
        return result?.Id ?? 0;
    }

    // <inheritdoc />
    public async Task<int> FetchUserIdByUsername(string username)
    {
        var getUserByUsernameQuery = new GetUserByUsernameQuery(username);
        var result = await userQueryService.Handle(getUserByUsernameQuery);
        return result?.Id ?? 0;
    }

    // <inheritdoc />
    public async Task<string> FetchUsernameByUserId(int userId)
    {
        var getUserByIdQuery = new GetUserByIdQuery(userId);
        var result = await userQueryService.Handle(getUserByIdQuery);
        return result?.Username ?? string.Empty;
    }
}