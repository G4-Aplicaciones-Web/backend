using backendNetCore.IAM.Domain.Model.Aggregates;
using backendNetCore.Shared.Domain.Repositories;

namespace backendNetCore.IAM.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    
    /// <summary>
    /// Finds a user by username. 
    /// </summary>
    /// <param name="username">
    /// The username to search for.
    /// </param>
    /// <returns>
    /// The user if found; otherwise, null.
    /// </returns>
    Task<User?> FindByUsernameAsync(string username);
    
    /// <summary>
    /// Checks if a user with the specified username exists. 
    /// </summary>
    /// <param name="username">
    /// The username to check for.
    /// </param>
    /// <returns>
    /// True if a user with the specified username exists; otherwise, false.
    /// </returns>
    bool ExistsByUsername(string username);

}