using System.Net.Mime;
using backendNetCore.IAM.Domain.Services;
using backendNetCore.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using backendNetCore.IAM.Interfaces.REST.Resources;
using backendNetCore.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace backendNetCore.IAM.Interfaces.REST;

/// <summary>
/// Authentication Controller 
/// </summary>
/// <remarks>
/// This controller provides the authentication endpoints.
/// The authentication endpoints are used to sign in and sign-up to the platform.
/// </remarks>
/// <param name="userCommandService">
/// <see cref="IUserCommandService"/> User command service
/// </param>
[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication endpoints")]
public class AuthenticationController(IUserCommandService userCommandService) : ControllerBase
{
    /// <summary>
    /// Sign in 
    /// </summary>
    /// <remarks>
    /// This endpoint is used to sign in to the platform.
    /// </remarks>
    /// <param name="resource">
    /// The <see cref="SignInResource"/> object.
    /// </param>
    /// <returns>
    /// The <see cref="AuthenticatedUserResource"/> object, containing the authenticated user and the token.
    /// </returns>
    [AllowAnonymous]
    [HttpPost("sign-in")]
    [SwaggerOperation(
        Summary = "Sign in",
        Description = "Sign in to the platform",
        OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "Authenticated user", typeof(AuthenticatedUserResource))]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
        var authenticatedUser = await userCommandService.Handle(signInCommand);
        var authenticatedUserResource = AuthenticatedUserResourceFromEntityAssembler
            .ToResourceFromEntity(authenticatedUser.user, authenticatedUser.token);
        return Ok(authenticatedUserResource);
    }

    /// <summary>
    /// Sign up 
    /// </summary>
    /// <remarks>
    /// This endpoint is used to sign up to the platform.
    /// </remarks>
    /// <param name="resource">
    /// The <see cref="SignUpResource"/> object.
    /// </param>
    /// <returns>
    /// A message indicating that the user was created successfully.
    /// </returns>
    [AllowAnonymous]
    [HttpPost("sign-up")]
    [SwaggerOperation(
        Summary = "Sign up",
        Description = "Sign up to the platform",
        OperationId = "SignUp")]
    [SwaggerResponse(StatusCodes.Status200OK, "User created successfully")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
        await userCommandService.Handle(signUpCommand);
        return Ok(new { message = "User created successfully" });
    }
}