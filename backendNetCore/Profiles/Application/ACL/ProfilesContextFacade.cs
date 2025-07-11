using backendNetCore.Profiles.Domain.Model.Aggregates;
using backendNetCore.Profiles.Domain.Model.Commands;
using backendNetCore.Profiles.Domain.Model.Entities;
using backendNetCore.Profiles.Domain.Model.Queries;
using backendNetCore.Profiles.Domain.Services;
using backendNetCore.Profiles.Interfaces.ACL;

namespace backendNetCore.Profiles.Application.ACL;

/// <summary>
/// Concrete implementation of the ProfilesContextFacade
/// </summary>
/// <param name="profileCommandService">Command service for profile operations</param>
/// <param name="profileQueryService">Query service for profile operations</param>
public class ProfilesContextFacade(
    IProfileCommandService profileCommandService,
    IProfileQueryService profileQueryService
    ) : IProfilesContextFacade
{
    public async Task<int> CreateProfile(string firstName, string lastName, string gender, double height, double weight, double score,
        int activityLevelId, int objectiveId)
    {
        var command = new CreateProfileCommand(firstName, lastName, gender, height, weight, score, activityLevelId, objectiveId);
        var profile = await profileCommandService.Handle(command);
        return profile?.Id ?? 0;
    }

    public async Task<Profile?> FetchById(int profileId)
    {
        var result = await profileQueryService.Handle(new GetProfileByIdQuery(profileId));
        return result;
    }

    public async Task<Objective?> FetchObjectiveByProfileId(int profileId)
    {
        var profile = await profileQueryService.Handle(new GetProfileByIdQuery(profileId));
        return profile?.Objective;
    }

    public async Task<string?> FetchObjectiveNameByProfileId(int profileId)
    {
        var objective = await FetchObjectiveByProfileId(profileId);
        return objective?.Name;
    }

    public async Task<bool> ExistsProfileById(int profileId)
    {
        var profile = await profileQueryService.Handle(new GetProfileByIdQuery(profileId));
        return profile != null;
    }

    public async Task<Profile?> FetchUserProfileById(int profileId)
    {
        return await profileQueryService.Handle(new GetProfileByIdQuery(profileId));
    }

    public async Task<Profile?> UpdateProfile(int profileId, double height, double weight, int activityLevelId, int objectiveId)
    {
        var command = new UpdateProfileCommand(profileId, height, weight, activityLevelId, objectiveId);
        return await profileCommandService.Handle(command);
    }

    public async Task<Profile?> AddAllergy(int profileId, string allergyName)
    {
        var command = new AddAllergyToProfileCommand(profileId, allergyName);
        return await profileCommandService.Handle(command);
    }

    public async Task<Profile?> RemoveAllergy(int profileId, string allergyName)
    {
        var command = new RemoveAllergyFromProfileCommand(profileId, allergyName);
        return await profileCommandService.Handle(command);
    }
    
}
