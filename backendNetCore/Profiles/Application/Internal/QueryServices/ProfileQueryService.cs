using backendNetCore.Profiles.Domain.Model.Aggregates;
using backendNetCore.Profiles.Domain.Model.Queries;
using backendNetCore.Profiles.Domain.Model.ValueObjects;
using backendNetCore.Profiles.Domain.Repositories;
using backendNetCore.Profiles.Domain.Services;

namespace backendNetCore.Profiles.Application.Internal.QueryServices;

/// <summary>
/// Profile query service 
/// </summary>
/// <param name="profileRepository">
/// Profile repository
/// </param>
public class ProfileQueryService(IProfileRepository profileRepository) : IProfileQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query)
    {
        return await profileRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await profileRepository.FindByIdAsync(query.ProfileId);
    }

    public async Task<Profile?> Handle(GetProfileByUserIdQuery query)
    {
        var userId = new UserId(query.UserId);
        return await profileRepository.FindByUserIdAsync(userId);   
    }
}