using backendNetCore.Shared.Domain.Repositories;
using backendNetCore.Tracking.Domain.Model.Commands;
using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Repositories;
using backendNetCore.Tracking.Domain.Services;
using backendNetCore.Shared.Domain.Repositories;

namespace backendNetCore.Tracking.Application.Internal.CommandServices;

/// <summary>
/// Tracking command service implementation
/// </summary>
/// <param name="trackingRepository">Tracking repository</param>
/// <param name="mealPlanEntryRepository">MealPlanEntry repository</param>
/// <param name="trackingGoalRepository">TrackingGoal repository</param>
/// <param name="macronutrientValuesRepository">TrackingMacronutrient repository</param>
/// <param name="mealPlanTypeRepository">MealPlanType repository</param>
/// <param name="unitOfWork">Unit of work</param>
public class TrackingCommandService(
    ITrackingRepository trackingRepository,
    IMealPlanEntryRepository mealPlanEntryRepository,
    ITrackingGoalRepository trackingGoalRepository,
    ITrackingMacronutrientRepository macronutrientValuesRepository,
    IMealPlanTypeRepository mealPlanTypeRepository,
    IUnitOfWork unitOfWork)
    : ITrackingCommandService
{
    public async Task<Domain.Model.Aggregates.Tracking> Handle(CreateTrackingCommand command)
    {
        if (await trackingRepository.ExistsByUserIdAsync(command.UserId))
            throw new ArgumentException($"Tracking already exists for user: {command.UserId}");

        var goal = await trackingGoalRepository.FindByUserIdAsync(command.UserId)
                   ?? throw new ArgumentException("Tracking goal not found");

        var consumed = new TrackingMacronutrient(0, 0, 0, 0);
        await macronutrientValuesRepository.AddAsync(consumed);

        var tracking = new Domain.Model.Aggregates.Tracking(command.UserId, DateTime.UtcNow, goal, consumed);
        await trackingRepository.AddAsync(tracking);
        await unitOfWork.CompleteAsync();

        return tracking;
    }

    public async Task<MealPlanEntry> Handle(CreateMealPlanEntryToTrackingCommand command)
    {
        var tracking = await trackingRepository.FindByUserIdAsync(command.UserId)
                       ?? throw new ArgumentException("Tracking not found");

        var type = await mealPlanTypeRepository.FindByNameAsync(command.MealPlanType)
                   ?? throw new ArgumentException("Invalid meal plan type");

        var entry = new MealPlanEntry(command.RecipeId, type, command.DayNumber);
        
        // Agregar la entrada a través del aggregate
        tracking.AddMealPlanEntry(entry);

        // Solo necesitamos actualizar el tracking, no el repository de MealPlanEntry
        trackingRepository.Update(tracking);
        await unitOfWork.CompleteAsync();

        return entry;
    }

    public async Task Handle(RemoveMealPlanEntryFromTrackingCommand command)
    {
        var tracking = await trackingRepository.FindByIdAsync(command.TrackingId)
                       ?? throw new ArgumentException("Tracking not found");

        // Buscar la entrada en el aggregate usando el método público
        var entry = tracking.GetMealPlanEntries()
            .FirstOrDefault(e => e.Id == command.MealPlanEntryId)
            ?? throw new ArgumentException("Entry not found");

        // Remover a través del aggregate y del repository
        tracking.RemoveMealPlanEntry(entry);
        mealPlanEntryRepository.Remove(entry);
        trackingRepository.Update(tracking);
        await unitOfWork.CompleteAsync();
    }

    public async Task<Domain.Model.Aggregates.Tracking?> Handle(UpdateMealPlanEntryInTrackingCommand command)
    {
        var tracking = await trackingRepository.FindByIdAsync(command.TrackingId);
        if (tracking is null) return null;

        // Buscar la entrada existente en el aggregate
        var oldEntry = tracking.GetMealPlanEntries()
            .FirstOrDefault(e => e.Id == command.MealPlanEntryId)
            ?? throw new ArgumentException("Old entry not found");

        var newType = await mealPlanTypeRepository.FindByNameAsync(command.MealPlanTypes)
            ?? throw new ArgumentException("Invalid meal plan type");

        // Actualizar a través del aggregate y repositories
        var updatedEntry = new MealPlanEntry(command.RecipeId, newType, command.DayNumber);
        
        tracking.RemoveMealPlanEntry(oldEntry);
        mealPlanEntryRepository.Remove(oldEntry);
        
        tracking.AddMealPlanEntry(updatedEntry);
        await mealPlanEntryRepository.AddAsync(updatedEntry);

        trackingRepository.Update(tracking);
        await unitOfWork.CompleteAsync();

        return tracking;
    }
}