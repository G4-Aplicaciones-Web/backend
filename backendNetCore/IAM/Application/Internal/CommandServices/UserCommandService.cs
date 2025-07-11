using backendNetCore.IAM.Application.Internal.OutboundServices;
using backendNetCore.IAM.Domain.Model.Aggregates;
using backendNetCore.IAM.Domain.Model.Commands;
using backendNetCore.IAM.Domain.Repositories;
using backendNetCore.IAM.Domain.Services;
using backendNetCore.Shared.Domain.Repositories;

namespace backendNetCore.IAM.Application.Internal.CommandServices;

/// <summary>
/// User command service 
/// </summary>
/// <remarks>
/// This class is responsible for handling user commands
/// </remarks>
/// <param name="userRepository">
/// The <see cref="IUserRepository"/> user repository
/// </param>
/// <param name="unitOfWork">
/// The <see cref="IUnitOfWork"/> unit of work
/// </param>
/// <param name="tokenService">
/// The <see cref="ITokenService"/> token service
/// </param>
/// <param name="hashingService">
/// The <see cref="IHashingService"/> hashing service
/// </param>
public class UserCommandService(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ITokenService tokenService,
    IHashingService hashingService
) : IUserCommandService
{
    // inheritDoc
    public async Task Handle(SignUpCommand command)
    {
        if (userRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} already exists");
        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashedPassword);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating the user: {e.Message}");
        }
    }

    // inheritDoc
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username);
        if (user is null) throw new Exception($"User {command.Username} not found");
        if (!hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid password");
        var token = tokenService.GenerateToken(user);
        return (user, token);
    }
}