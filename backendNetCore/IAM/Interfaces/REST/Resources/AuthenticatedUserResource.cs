namespace backendNetCore.IAM.Interfaces.REST.Resources;

/// <summary>
/// Authenticated User Resource 
/// </summary>
/// <param name="Id">
/// The unique identifier of the user.
/// </param>
/// <param name="Username">
/// The username of the user.
/// </param>
/// <param name="Token">
/// The token of the user.
/// </param>
public record AuthenticatedUserResource(int Id, string Username, string Token);