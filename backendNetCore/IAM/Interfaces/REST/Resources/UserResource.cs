namespace backendNetCore.IAM.Interfaces.REST.Resources;

/// <summary>
/// User Resource 
/// </summary>
/// <param name="Id">
/// The unique identifier of the user.
/// </param>
/// <param name="Username">
/// The username of the user.
/// </param>
public record UserResource(int Id, string Username);