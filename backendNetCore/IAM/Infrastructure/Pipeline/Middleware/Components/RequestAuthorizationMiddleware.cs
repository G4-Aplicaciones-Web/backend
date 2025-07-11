using backendNetCore.IAM.Application.Internal.OutboundServices;
using backendNetCore.IAM.Domain.Model.Queries;
using backendNetCore.IAM.Domain.Services;
using backendNetCore.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace backendNetCore.IAM.Infrastructure.Pipeline.Middleware.Components;
/// <summary>
/// Request authorization middleware 
/// </summary>
/// <param name="next">
/// <see cref="RequestDelegate"/> Next middleware in pipeline
/// </param>
public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    /// <summary>
    /// Invoke middleware 
    /// </summary>
    /// <remarks>
    /// This middleware is responsible for authorizing requests. It checks if the request is allowed to be anonymous. If it is, it skips authorization. If it is not, it validates the token in the request header and retrieves the user associated with the token. It then updates the context with the user and continues to the next middleware in the pipeline.
    /// </remarks>
    /// <param name="context">
    /// <see cref="HttpContext"/> HTTP context
    /// </param>
    /// <param name="userQueryService">
    /// <see cref="IUserQueryService"/> User query service
    /// </param>
    /// <param name="tokenService">
    /// <see cref="ITokenService"/> Token service
    /// </param>
    /// <exception cref="Exception">
    /// Thrown when the token is null or invalid
    /// </exception>
    
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        Console.WriteLine("Entering InvokeAsync");
        Console.WriteLine($"Request Path: {context.Request.Path}");
        Console.WriteLine($"Request Method: {context.Request.Method}");
        
        var endpoint = context.Request.HttpContext.GetEndpoint();
        Console.WriteLine($"Endpoint: {endpoint?.DisplayName ?? "null"}");
        
        if (endpoint == null)
        {
            Console.WriteLine("WARNING: Endpoint is null!");
            Console.WriteLine($"All Request Headers:");
            foreach (var header in context.Request.Headers)
            {
                Console.WriteLine($"Header: {header.Key} = {header.Value}");
            }
            await next(context);
            return;
        }

        var metadata = endpoint.Metadata;
        Console.WriteLine($"Metadata Count: {metadata.Count}");
        foreach (var item in metadata)
        {
            Console.WriteLine($"Metadata Type: {item.GetType().FullName}");
        }

        var allowAnonymous = metadata.Any(m => m.GetType() == typeof(AllowAnonymousAttribute));
        Console.WriteLine($"AllowAnonymous: {allowAnonymous}");
        
        if (allowAnonymous)
        {
            Console.WriteLine("Skipping authorization - Anonymous access allowed");
            await next(context);
            return;
        }

        Console.WriteLine("Checking Authorization header");
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        Console.WriteLine($"Authorization Header: {authHeader ?? "null"}");

        var token = authHeader?.Split(" ").Last();
        Console.WriteLine($"Token present: {!string.IsNullOrEmpty(token)}");

        if (token is null)
        {
            Console.WriteLine("Token is null - throwing exception");
            throw new Exception("Null or invalid token");
        }
        
        try
        {
            var userId = await tokenService.ValidateToken(token);
            Console.WriteLine($"Token validation result - UserId: {userId?.ToString() ?? "null"}");
            
            if (userId is null)
            {
                Console.WriteLine("UserId is null after token validation");
                throw new Exception("Invalid token");
            }
            
            var getUserByIdQuery = new GetUserByIdQuery(userId.Value);
            var user = await userQueryService.Handle(getUserByIdQuery);
            Console.WriteLine($"User retrieved: {user != null}");
            
            Console.WriteLine("Updating context with user information");
            context.Items["User"] = user;
            
            Console.WriteLine("Continuing to next middleware");
            await next(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred during token processing: {ex.GetType().Name}");
            Console.WriteLine($"Exception message: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }
}