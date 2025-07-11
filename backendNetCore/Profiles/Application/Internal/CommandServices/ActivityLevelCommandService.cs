using backendNetCore.Profiles.Domain.Model.Commands;
using backendNetCore.Profiles.Domain.Model.Entities;
using backendNetCore.Profiles.Domain.Repositories;
using backendNetCore.Profiles.Domain.Services;
using backendNetCore.Shared.Domain.Repositories;

namespace backendNetCore.Profiles.Application.Internal.CommandServices;

public class ActivityLevelCommandService(IActivityLevelRepository activityLevelRepository, IUnitOfWork unitOfWork) : IActivityLevelCommandService
{
    public async Task<ActivityLevel?> Handle(CreateActivityLevelCommand command)
    {
        var activityLevel = new ActivityLevel(command.Name, command.Description, command.ActivityFactor);;
        await activityLevelRepository.AddAsync(activityLevel);
        await unitOfWork.CompleteAsync();
        return activityLevel;
    }
}