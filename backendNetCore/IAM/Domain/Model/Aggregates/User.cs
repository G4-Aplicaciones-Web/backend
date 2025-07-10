namespace backendNetCore.IAM.Domain.Model.Aggregates;

/// <summary>
/// User aggregate root 
/// </summary>
/// <param name="username">
/// The username of the user
/// </param>
/// <param name="passwordHash">
/// The password hash of the user
/// </param>
public class User(string username, string passwordHash)
{
    public int Id { get; }

    public string Username { get; private set; } = username;
    
    public string PasswordHash { get; private set; } = passwordHash;

    /// <summary>
    /// Update the username of the user 
    /// </summary>
    /// <param name="username">
    /// The new username to update
    /// </param>
    /// <returns>
    /// The updated user
    /// </returns>
    public User UpdateUsername(string username)
    {
        this.Username = username;
        return this;
    }

    /// <summary>
    /// Update the password hash of the user
    /// </summary>
    /// <param name="passwordHash">
    /// The new password hash to update
    /// </param>
    /// <returns>
    /// The updated user
    /// </returns>
    public User UpdatePasswordHash(string passwordHash)
    {
        this.PasswordHash = passwordHash;
        return this;
    }

}