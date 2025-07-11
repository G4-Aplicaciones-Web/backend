namespace backendNetCore.IAM.Domain.Model.Queries;

/// <summary>
/// Get user by id query 
/// </summary>
/// <param name="UserId">
/// The user id to get
/// </param>
public record GetUserByIdQuery(int UserId);