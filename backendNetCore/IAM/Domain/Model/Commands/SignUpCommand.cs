namespace backendNetCore.IAM.Domain.Model.Commands;

/// <summary>
/// Sign up command 
/// </summary>
/// <param name="Username">
/// The username of the user
/// </param>
/// <param name="Password">
/// The password of the user
/// </param>
public record SignUpCommand(string Username, string Password);