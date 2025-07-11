using backendNetCore.IAM.Domain.Model.Aggregates;
using backendNetCore.IAM.Domain.Model.Queries;

namespace backendNetCore.IAM.Domain.Services;


/// <summary>
/// Represents the user query service. 
/// </summary>
/// <remarks>
/// This interface is used to handle user queries.
/// </remarks>
public interface IUserQueryService
{
    /// <summary>
    /// Handles the get user by id query. 
    /// </summary>
    /// <param name="query">
    /// The query to handle.
    /// </param>
    /// <returns>
    /// The <see cref="User"/> user if found; otherwise, null.
    /// </returns>
    Task<User?> Handle(GetUserByIdQuery query);
    
    /// <summary>
    /// Handles the get all users query. 
    /// </summary>
    /// <param name="query">
    /// The query to handle.
    /// </param>
    /// <returns>
    /// The an <see cref="IEnumerable{T}"/>list of <see cref="User"/> users.
    /// </returns>
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
    
    /// <summary>
    /// Handles the get user by username query. 
    /// </summary>
    /// <param name="query">
    /// The query to handle.
    /// </param>
    /// <returns>
    /// The <see cref="User"/> user if found; otherwise, null.
    /// </returns>
    Task<User?> Handle(GetUserByUsernameQuery query);
}