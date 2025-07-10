namespace backendNetCore.IAM.Domain.Model.Commands;

/// <summary>
/// Sign in command 
/// </summary>
/// <param name="Username">
/// The username of the user
/// </param>
/// <param name="Password">
/// The password of the user
/// </param>
public record SignInCommand(string Username, string Password);