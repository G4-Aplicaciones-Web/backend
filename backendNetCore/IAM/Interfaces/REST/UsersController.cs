using System.Net.Mime;
using backendNetCore.IAM.Domain.Model.Queries;
using backendNetCore.IAM.Domain.Services;
using backendNetCore.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using backendNetCore.IAM.Interfaces.REST.Resources;
using backendNetCore.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace backendNetCore.IAM.Interfaces.REST;

/// <summary>
/// Available User endpoints 
/// </summary>
/// <remarks>
/// Available User endpoints. This controller is responsible for handling user related requests.
/// </remarks>
/// <param name="userQueryService">
/// <see cref="IUserQueryService"/> User query service
/// </param>
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User endpoints")]
public class UsersController(IUserQueryService userQueryService) : ControllerBase
{
    /// <summary>
    /// Get user by id 
    /// </summary>
    /// <remarks>
    /// Get user by id. This endpoint is responsible for getting a user by id.
    /// </remarks>
    /// <param name="id">
    /// <see cref="int"/> User id
    /// </param>
    /// <returns>
    /// <see cref="IActionResult"/> with the <see cref="UserResource"/> user, if found.
    /// It returns <see cref="NotFoundResult"/> if the user is not found. 
    /// </returns>
    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary ="Get user by id",
        Description = "Get user by id",
        OperationId = "GetUserById")]
    [SwaggerResponse(StatusCodes.Status200OK, "User found", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var user = await userQueryService.Handle(getUserByIdQuery);
        if (user is null) return NotFound();
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(userResource);
    }

    /// <summary>
    /// Get all users 
    /// </summary>
    /// <remarks>
    /// Get all users. This endpoint is responsible for getting all users.
    /// </remarks>
    /// <returns>
    /// <see cref="IActionResult"/> with the <see cref="IEnumerable{UserResource}"/> users.
    /// </returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all users",
        Description = "Get all users",
        OperationId = "GetAllUsers")]
    [SwaggerResponse(StatusCodes.Status200OK, "Users found", typeof(IEnumerable<UserResource>))]
    public async Task<IActionResult> GetAllUsers()
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var users = await userQueryService.Handle(getAllUsersQuery);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }
    
}