using backendNetCore.IAM.Domain.Model.Commands;
using backendNetCore.IAM.Interfaces.REST.Resources;

namespace backendNetCore.IAM.Interfaces.REST.Transform;

/// <summary>
/// SignUp Command From Resource Assembler 
/// </summary>
public static class SignUpCommandFromResourceAssembler
{
    /// <summary>
    /// This method converts a SignInResource to a SignUpCommand. 
    /// </summary>
    /// <param name="resource">
    /// The <see cref="SignInResource"/> to convert.
    /// </param>
    /// <returns>
    /// The <see cref="SignUpCommand"/> object.
    /// </returns>
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password);
    }
}