using backendNetCore.Profiles.Domain.Model.Aggregates;
using backendNetCore.Profiles.Domain.Model.Queries;

namespace backendNetCore.Profiles.Domain.Services;

/// <summary>
/// Profile query service 
/// </summary>
public interface IProfileQueryService
{
    /// <summary>
    /// Handle get all profiles 
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetAllProfilesQuery"/> query
    /// </param>
    /// <returns>
    /// A list of <see cref="Profile"/> objects
    /// </returns>
    Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query);
    
    /// <summary>
    /// Handle get profile by id 
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetProfileByIdQuery"/> query
    /// </param>
    /// <returns><
    /// A <see cref="Profile"/> object or null
    /// /returns>
    Task<Profile?> Handle(GetProfileByIdQuery query);
    
    Task<Profile?> Handle(GetProfileByUserIdQuery query);
}