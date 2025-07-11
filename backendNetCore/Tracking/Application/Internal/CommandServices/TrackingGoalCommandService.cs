using backendNetCore.Shared.Domain.Repositories;
using backendNetCore.Tracking.Domain.Model.Commands;
using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Repositories;
using backendNetCore.Tracking.Domain.Services;
using backendNetCore.Shared.Domain.Repositories;

namespace backendNetCore.Tracking.Application.Internal.CommandServices;

public class TrackingGoalCommandService : ITrackingGoalCommandService
{
    private readonly ITrackingGoalRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public TrackingGoalCommandService(
        ITrackingGoalRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TrackingGoal> Handle(CreateTrackingGoalCommand command)
    {
        var goal = new TrackingGoal(command.UserId, command.Macronutrient);
        await _repository.AddAsync(goal);
        await _unitOfWork.CompleteAsync();
        return goal;
    }

    public async Task<TrackingGoal> Handle(CreateTrackingGoalByObjectiveCommand command)
    {
        // Verificar si ya existe un goal para este usuario
        var existingGoal = await _repository.FindByUserIdAsync(command.UserId);
        if (existingGoal != null)
        {
            throw new InvalidOperationException($"A tracking goal already exists for user {command.UserId}");
        }

        try
        {
            // Usar GoalType.FromDisplayName para convertir el string al GoalType correspondiente
            var goalType = GoalType.FromDisplayName(command.GoalType);
            var goal = new TrackingGoal(command.UserId, goalType);
            await _repository.AddAsync(goal);
            await _unitOfWork.CompleteAsync();
            return goal;
        }
        catch (ArgumentException ex)
        {
            throw new InvalidOperationException($"Invalid goal type: {command.GoalType}", ex);
        }
    }

    public async Task<TrackingGoal> Handle(UpdateTrackingGoalCommand command)
    {
        var trackingGoal = await _repository.FindByUserIdAsync(command.UserId);
        if (trackingGoal == null)
            throw new InvalidOperationException($"No se encontró una meta de seguimiento para el usuario {command.UserId}");

        trackingGoal.UpdateGoalType(command.GoalType);
        await _unitOfWork.CompleteAsync();
        
        return trackingGoal;
    }
}