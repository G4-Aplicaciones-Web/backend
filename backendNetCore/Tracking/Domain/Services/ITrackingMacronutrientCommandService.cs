using backendNetCore.Tracking.Domain.Model.Commands;
using backendNetCore.Tracking.Domain.Model.Entities;

namespace backendNetCore.Tracking.Domain.Services;

public interface ITrackingMacronutrientCommandService
{
    Task<TrackingMacronutrient> Handle(CreateMacronutrientValuesCommand command);
}