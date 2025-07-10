using backendNetCore.IAM.Domain.Model.Aggregates;
using backendNetCore.IAM.Domain.Model.Commands;

namespace backendNetCore.IAM.Domain.Services;


/// <summary>
/// User command service
/// </summary>
/// <remarks>
/// This service is responsible for handling user commands.
/// </remarks>  
public interface IUserCommandService
{
    /// <summary>
    /// Handle sign-up command 
    /// </summary>
    /// <remarks>
    /// This method is responsible for handling the sign-up command.
    /// It checks if the username already exists, hashes the password, creates a new user, and adds it to the repository.
    /// </remarks> 
    /// <param name="command">
    /// The sign-up command
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation
    /// </returns>
    Task Handle(SignUpCommand command);
    
    /// <summary>
    /// Handle sign-in command 
    /// </summary>
    /// <remarks>
    /// This method is responsible for handling the sign-in command.
    /// It finds the user by username, verifies the password, generates a token, and returns the user and token.
    /// </remarks> 
    /// <param name="command">
    /// The sign-in command
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the <see cref="User"/> user and generated token
    /// </returns>
    Task<(User user, string token)> Handle(SignInCommand command);
}