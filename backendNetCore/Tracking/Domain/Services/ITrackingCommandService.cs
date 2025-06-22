using AlimentateplusPlatform.API.Tracking.Domain.Model.Aggregates;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Commands;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;

namespace AlimentateplusPlatform.API.Tracking.Domain.Services;

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