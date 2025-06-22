using AlimentateplusPlatform.API.Tracking.Domain.Model.Commands;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;

namespace AlimentateplusPlatform.API.Tracking.Domain.Services;

public interface ITrackingGoalCommandService
{
    Task<TrackingGoal> Handle(CreateTrackingGoalCommand command);
}