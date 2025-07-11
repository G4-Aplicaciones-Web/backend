using backendNetCore.Tracking.Domain.Model.Entities;

namespace backendNetCore.Tracking.Domain.Services;

public interface IExternalProfileService
{
    Task<bool> ExistsProfileById(int profileId);
    Task<string?> GetObjectiveNameByProfileId(int profileId);
    Task<bool> ExistsUserById(int userId);
    Task<TrackingGoal?> CreateTrackingGoalBasedOnProfile(int profileId, int userId);
}