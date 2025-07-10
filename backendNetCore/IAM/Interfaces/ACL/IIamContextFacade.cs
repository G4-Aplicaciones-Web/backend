namespace backendNetCore.IAM.Interfaces.ACL;


/// <summary>
/// IAM Context Facade 
/// </summary>
/// <remarks>
/// This facade is responsible for handling the context of the IAM module.
/// It provides a simplified interface for the IAM module's context operations.
/// It allows the application layer to interact with the IAM module's context
/// without knowing the details of the implementation.
/// </remarks>
public interface IIamContextFacade
{
    /// <summary>
    /// Create a new user 
    /// </summary>
    /// <remarks>
    /// This method creates a new user with the given username and password.
    /// </remarks>
    /// <param name="username">
    /// The username of the new user
    /// </param>
    /// <param name="password">
    /// The password of the new user
    /// </param>
    /// <returns>
    /// The ID of the newly created user if successful, otherwise 0
    /// </returns>
    Task<int> CreateUser(string username, string password);
    
    /// <summary>
    /// Fetch the user ID by username 
    /// </summary>
    /// <remarks>
    /// This method fetches the user ID of the user with the given username.
    /// </remarks>
    /// <param name="username">
    /// The username of the user
    /// </param>
    /// <returns>
    /// The ID of the user with the given username if found, otherwise 0
    /// </returns>
    Task<int> FetchUserIdByUsername(string username);
    
    /// <summary>
    /// Fetch the username by user ID 
    /// </summary>
    /// <remarks>
    /// This method fetches the username of the user with the given user ID.
    /// </remarks>
    /// <param name="userId">
    /// The ID of the user
    /// </param>
    /// <returns>
    /// The username of the user with the given user ID if found, otherwise an empty string
    /// </returns>
    Task<string> FetchUsernameByUserId(int userId);
}