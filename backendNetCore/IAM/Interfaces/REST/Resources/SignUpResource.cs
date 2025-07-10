namespace backendNetCore.IAM.Interfaces.REST.Resources;

/// <summary>
/// SignUp Resource 
/// </summary>
/// <param name="Username">
/// The username of the user.
/// </param>
/// <param name="Password">
/// The password of the user.
/// </param>
public record SignUpResource(string Username, string Password);