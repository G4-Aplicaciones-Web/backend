namespace backendNetCore.IAM.Application.Internal.OutboundServices;
/// <summary>
/// Service to hash and verify passwords 
/// </summary>
public interface IHashingService
{
    /// <summary>
    /// Hashes the password 
    /// </summary>
    /// <param name="password">
    /// The password to hash
    /// </param>
    /// <returns>
    /// The hashed password
    /// </returns>
    string HashPassword(string password);
    
    /// <summary>
    /// Verifies the password against the password hash 
    /// </summary>
    /// <param name="password">
    /// The password to verify
    /// </param>
    /// <param name="passwordHash">
    /// The password hash to verify against
    /// </param>
    /// <returns>
    /// True if the password is verified, false otherwise
    /// </returns>
    bool VerifyPassword(string password, string passwordHash);
}