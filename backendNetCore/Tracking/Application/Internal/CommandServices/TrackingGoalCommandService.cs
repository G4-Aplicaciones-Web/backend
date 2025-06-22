using AlimentateplusPlatform.API.Shared.Domain.Repositories;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Commands;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;
using AlimentateplusPlatform.API.Tracking.Domain.Repositories;
using AlimentateplusPlatform.API.Tracking.Domain.Services;
using backendNetCore.Shared.Domain.Repositories;

namespace AlimentateplusPlatform.API.Tracking.Application.Internal.CommandServices;

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