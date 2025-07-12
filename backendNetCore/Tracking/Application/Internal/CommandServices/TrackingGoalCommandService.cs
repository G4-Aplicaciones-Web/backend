using backendNetCore.IAM.Interfaces.ACL;
using backendNetCore.Shared.Domain.Repositories;
using backendNetCore.Tracking.Domain.Model.Commands;
using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Model.ValueObjects;
using backendNetCore.Tracking.Domain.Repositories;
using backendNetCore.Tracking.Domain.Services;

namespace backendNetCore.Tracking.Application.Internal.CommandServices;

public class TrackingGoalCommandService : ITrackingGoalCommandService
{
    private readonly ITrackingGoalRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIamContextFacade _iamContextFacade;

    public TrackingGoalCommandService(
        ITrackingGoalRepository repository,
        IUnitOfWork unitOfWork,
        IIamContextFacade iamContextFacade)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _iamContextFacade = iamContextFacade;
    }

    public async Task<TrackingGoal> Handle(CreateTrackingGoalCommand command)
    {
        // Validar que el usuario existe en el contexto IAM
        await ValidateUserExistsInIam(command.UserId);

        var goal = new TrackingGoal(command.UserId, command.Macronutrient);
        await _repository.AddAsync(goal);
        await _unitOfWork.CompleteAsync();
        return goal;
    }

    public async Task<TrackingGoal> Handle(CreateTrackingGoalByObjectiveCommand command)
    {
        // Validar que el usuario existe en el contexto IAM
        await ValidateUserExistsInIam(command.UserId);

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
        // Validar que el usuario existe en el contexto IAM
        await ValidateUserExistsInIam(command.UserId);

        var trackingGoal = await _repository.FindByUserIdAsync(command.UserId);
        if (trackingGoal == null)
            throw new InvalidOperationException($"No se encontró una meta de seguimiento para el usuario {command.UserId}");

        trackingGoal.UpdateGoalType(command.GoalType);
        await _unitOfWork.CompleteAsync();
        
        return trackingGoal;
    }

    /// <summary>
    /// Validates that the user exists in the IAM context
    /// </summary>
    /// <param name="userId">The user ID to validate</param>
    /// <exception cref="InvalidOperationException">Thrown when the user does not exist in IAM</exception>
    private async Task ValidateUserExistsInIam(UserId userId)
    {
        var username = await _iamContextFacade.FetchUsernameByUserId(userId.Id);
        if (string.IsNullOrEmpty(username))
        {
            throw new InvalidOperationException($"User with ID {userId.Id} does not exist in IAM context");
        }
    }
}