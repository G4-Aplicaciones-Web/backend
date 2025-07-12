using backendNetCore.Profiles.Domain.Model.Aggregates;
using backendNetCore.Profiles.Domain.Model.Commands;
using backendNetCore.Profiles.Domain.Repositories;
using backendNetCore.Profiles.Domain.Services;
using backendNetCore.Shared.Domain.Repositories;

namespace backendNetCore.Profiles.Application.Internal.CommandServices;

/// <summary>
/// Profile command service 
/// </summary>
/// <param name="profileRepository">
/// Profile repository
/// </param>
/// <param name="unitOfWork">
/// Unit of work
/// </param>
public class ProfileCommandService(
    IProfileRepository profileRepository, 
    IObjectiveRepository objectiveRepository,
    IActivityLevelRepository activityLevelRepository,
    IUnitOfWork unitOfWork) 
    : IProfileCommandService
{
    /// <inheritdoc />
    public async Task<Profile?> Handle(CreateProfileCommand command)
    {
        var objective = await objectiveRepository.FindByIdAsync(command.ObjectiveId);
        if (objective is null) throw new Exception("Objective not found");
        var activityLevel = await activityLevelRepository.FindByIdAsync(command.ActivityLevelId);
        if (activityLevel is null) throw new Exception("Activity level not found");
        var profile = new Profile(command);
        try
        {
            await profileRepository.AddAsync(profile);
            await unitOfWork.CompleteAsync();
            profile.ActivityLevel = activityLevel;
            profile.Objective = objective;
            return profile;
        } catch (Exception e)
        {
            // Log error
            return null;
        }
    }

    public async Task<Profile?> Handle(UpdateProfileCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.ProfileId);
        if (profile is null) return null;

        var objective = await objectiveRepository.FindByIdAsync(command.ObjectiveId);
        if (objective is null) throw new Exception("Objective not found");

        var activityLevel = await activityLevelRepository.FindByIdAsync(command.ActivityLevelId);
        if (activityLevel is null) throw new Exception("Activity level not found");

        profile.UpdateProfile(command.Height, command.Weight, command.ActivityLevelId, command.ObjectiveId);

        try
        {
            profileRepository.Update(profile);
            await unitOfWork.CompleteAsync();

            // Asignación manual para evitar nulls en el assembler
            profile.ActivityLevel = activityLevel;
            profile.Objective = objective;

            return profile;
        }
        catch (Exception)
        {
            // Log error
            return null;
        }
    }



    public async Task<Profile?> Handle(AddAllergyToProfileCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.ProfileId);
        if (profile is null) return null;

        profile.AddAllergy(command.AllergyName);

        try
        {
            profileRepository.Update(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        }
        catch (Exception)
        {
            // Log error
            return null;
        }
    }


    public async Task<Profile?> Handle(RemoveAllergyFromProfileCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.ProfileId);
        if (profile is null) return null;

        profile.RemoveAllergy(command.AllergyName);

        try
        {
            profileRepository.Update(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        }
        catch (Exception)
        {
            // Log error
            return null;
        }
    }
}