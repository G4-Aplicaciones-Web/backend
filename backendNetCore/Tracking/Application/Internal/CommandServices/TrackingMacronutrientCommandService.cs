using AlimentateplusPlatform.API.Shared.Domain.Repositories;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Commands;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;
using AlimentateplusPlatform.API.Tracking.Domain.Repositories;
using AlimentateplusPlatform.API.Tracking.Domain.Services;

namespace AlimentateplusPlatform.API.Tracking.Application.Internal.CommandServices;

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