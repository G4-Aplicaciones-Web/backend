using backendNetCore.IAM.Domain.Model.Aggregates;

namespace backendNetCore.IAM.Application.Internal.OutboundServices;

/// <summary>
/// Service to generate and validate tokens 
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates a token for the user 
    /// </summary>
    /// <param name="user">
    /// The <see cref="User"/> user to generate the token for
    /// </param>
    /// <returns>
    /// The generated token
    /// </returns>
    string GenerateToken(User user);
    
    /// <summary>
    /// Validates the token
    /// </summary>
    /// <param name="token">
    /// The token to validate
    /// </param>
    /// <returns>
    /// The user id if the token is valid, null otherwise
    /// </returns>  
    Task<int?> ValidateToken(string token);
}