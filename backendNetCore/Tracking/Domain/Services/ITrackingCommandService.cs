using backendNetCore.Tracking.Domain.Model.Commands;
using backendNetCore.Tracking.Domain.Model.Entities;

namespace backendNetCore.Tracking.Domain.Services;

/// <summary>
/// Tracking command service interface
/// </summary>
public interface ITrackingCommandService
{
    Task<Model.Aggregates.Tracking> Handle(CreateTrackingCommand command);
    Task<MealPlanEntry> Handle(CreateMealPlanEntryToTrackingCommand command);
    Task Handle(RemoveMealPlanEntryFromTrackingCommand command);
    Task<Model.Aggregates.Tracking?> Handle(UpdateMealPlanEntryInTrackingCommand command);
}