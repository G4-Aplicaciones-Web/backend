using backendNetCore.Profiles.Domain.Model.Aggregates;
using backendNetCore.Profiles.Domain.Model.Entities;
using backendNetCore.Profiles.Interfaces.REST.Resources;

namespace backendNetCore.Profiles.Interfaces.ACL;

/// <summary>
/// Facade for the profiles context
/// </summary>
public interface IProfilesContextFacade
{
    Task<int> CreateProfile(
        string firstName,
        string lastName,
        string gender,
        double height,
        double weight,
        double score,
        int activityLevelId,
        int objectiveId
    );

    Task<Profile?> FetchById(int profileId);
    Task<bool> ExistsProfileById(int profileId);
    Task<Objective?> FetchObjectiveByProfileId(int profileId);
    Task<string?> FetchObjectiveNameByProfileId(int profileId);

    Task<Profile?> AddAllergy(int profileId, string ingredientName);
    Task<Profile?> RemoveAllergy(int profileId, string ingredientName);
}