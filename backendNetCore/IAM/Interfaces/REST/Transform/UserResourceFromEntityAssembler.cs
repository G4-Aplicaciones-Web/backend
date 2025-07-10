using backendNetCore.IAM.Domain.Model.Aggregates;
using backendNetCore.IAM.Interfaces.REST.Resources;

namespace backendNetCore.IAM.Interfaces.REST.Transform;

/// <summary>
/// User Resource From Entity Assembler 
/// </summary>
public static class UserResourceFromEntityAssembler
{
    /// <summary>
    /// This method converts a User entity to a UserResource. 
    /// </summary>
    /// <param name="user">
    /// The <see cref="User"/> entity to convert.
    /// </param>
    /// <returns>
    /// The <see cref="UserResource"/> object.
    /// </returns>
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(user.Id, user.Username);
    }
}