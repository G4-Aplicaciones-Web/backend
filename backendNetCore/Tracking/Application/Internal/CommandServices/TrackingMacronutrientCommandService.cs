using backendNetCore.Shared.Domain.Repositories;
using backendNetCore.Tracking.Domain.Model.Commands;
using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Repositories;
using backendNetCore.Tracking.Domain.Services;

namespace backendNetCore.Tracking.Application.Internal.CommandServices;

public class TrackingMacronutrientCommandService(
    ITrackingMacronutrientRepository repository,
    IUnitOfWork unitOfWork)
    : ITrackingMacronutrientCommandService
{
    public async Task<TrackingMacronutrient> Handle(CreateMacronutrientValuesCommand command)
    {
        var values = new TrackingMacronutrient(
            command.Calories,
            command.Carbs,
            command.Proteins,
            command.Fats
        );

        await repository.AddAsync(values);
        await unitOfWork.CompleteAsync();

        return values;
    }
}