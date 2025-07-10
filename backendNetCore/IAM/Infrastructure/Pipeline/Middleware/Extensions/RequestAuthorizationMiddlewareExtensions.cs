using backendNetCore.IAM.Infrastructure.Pipeline.Middleware.Components;

namespace backendNetCore.IAM.Infrastructure.Pipeline.Middleware.Extensions;

/// <summary>
/// Request authorization middleware extensions 
/// </summary>
/// <remarks>
/// This class contains extension methods for the <see cref="RequestAuthorizationMiddleware"/> class
/// </remarks>
public static class RequestAuthorizationMiddlewareExtensions
{
    /// <summary>
    /// Use request authorization middleware 
    /// </summary>
    /// <param name="builder">
    /// <see cref="IApplicationBuilder"/> Application builder
    /// </param>
    /// <returns>
    /// <see cref="IApplicationBuilder"/> Application builder
    /// </returns>
    public static IApplicationBuilder UseRequestAuthorization(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestAuthorizationMiddleware>();
    }
}