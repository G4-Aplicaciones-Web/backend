using backendNetCore.IAM.Domain.Model.Aggregates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace backendNetCore.IAM.Infrastructure.Pipeline.Middleware.Attributes;

/// <summary>
/// Authorize attribute 
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter

{
    /// <summary>
    /// On authorization 
    /// </summary>
    /// <remarks>
    /// This method is called when the authorization filter is executed
    /// </remarks>
    /// <param name="context">
    /// <see cref="AuthorizationFilterContext"/> Authorization filter context
    /// </param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Verify if the endpoint has AllowAnonymous attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        // If the endpoint has AllowAnonymous attribute, skip authorization
        if (allowAnonymous)
        {
            Console.WriteLine("Skipping authorization");
            return;
        }
        // Verify if the user is authenticated
        var user = (User?)context.HttpContext.Items["User"];
        // If the user is not authenticated, return 401 Unauthorized
        if (user is null) context.Result = new UnauthorizedResult();
    }
}