using backendNetCore.Shared.Domain.Repositories;
using backendNetCore.Tracking.Domain.Model.Commands;
using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Repositories;
using backendNetCore.Tracking.Domain.Services;
using backendNetCore.Shared.Domain.Repositories;

namespace backendNetCore.Tracking.Application.Internal.CommandServices;

public class TrackingGoalCommandService(
    ITrackingGoalRepository repository,
    IUnitOfWork unitOfWork)
    : ITrackingGoalCommandService
{
    public async Task<TrackingGoal> Handle(CreateTrackingGoalCommand command)
    {
        var goal = new TrackingGoal(command.UserId, command.Macronutrient);
        await repository.AddAsync(goal);
        await unitOfWork.CompleteAsync();
        return goal;
    }
}