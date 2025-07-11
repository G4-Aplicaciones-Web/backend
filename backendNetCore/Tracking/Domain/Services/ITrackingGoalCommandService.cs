using backendNetCore.Tracking.Domain.Model.Commands;
using backendNetCore.Tracking.Domain.Model.Entities;

namespace backendNetCore.Tracking.Domain.Services;

public interface ITrackingGoalCommandService
{
    Task<TrackingGoal> Handle(CreateTrackingGoalCommand command);
    Task<TrackingGoal> Handle(UpdateTrackingGoalCommand command);

    Task<TrackingGoal> Handle(CreateTrackingGoalByObjectiveCommand command);
}