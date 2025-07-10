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
        var tracking = await trackingRepository.FindByIdWithEntriesAsync(command.TrackingId); // ✅ Cambiar aquí
        if (tracking is null)
            throw new ArgumentException("Tracking not found");

        var entry = tracking.GetMealPlanEntries()
                        .FirstOrDefault(e => e.Id == command.MealPlanEntryId)
                    ?? throw new ArgumentException("Entry not found");

        tracking.RemoveMealPlanEntry(entry);
        mealPlanEntryRepository.Remove(entry); // <- necesario para eliminarlo de la BD
        trackingRepository.Update(tracking);

        await unitOfWork.CompleteAsync();
    }


    public async Task<Domain.Model.Aggregates.Tracking?> Handle(UpdateMealPlanEntryInTrackingCommand command)
    {
        // 1. Primero obtener la entrada existente para conseguir el trackingId
        var existingEntry = await mealPlanEntryRepository.FindByIdAsync(command.MealPlanEntryId);
        if (existingEntry is null)
            throw new ArgumentException("Entry not found");

        // 2. Ahora cargar el tracking usando el trackingId de la entrada existente
        var tracking = await trackingRepository.FindByIdWithEntriesAsync(existingEntry.TrackingId);
        if (tracking is null)
            throw new ArgumentException("Tracking not found");

        // 🔍 Debug
        Console.WriteLine($"MealPlanEntriesInternal count: {tracking.MealPlanEntriesInternal?.Count ?? 0}");
        Console.WriteLine($"GetMealPlanEntries count: {tracking.GetMealPlanEntries().Count}");
    
        // 3. Obtener la entrada del tracking (ya sabemos que existe)
        var entry = tracking.GetMealPlanEntries()
                        .FirstOrDefault(e => e.Id == command.MealPlanEntryId)
                    ?? throw new ArgumentException("Entry not found in tracking");

        // 4. Validar el tipo de meal plan
        var type = await mealPlanTypeRepository.FindByNameAsync(command.MealPlanType)
                   ?? throw new ArgumentException("Invalid meal plan type");

        // 🔄 Actualizamos propiedades
        entry.RecipeId = command.RecipeId;
        entry.MealPlanType = type;
        entry.DayNumber = command.DayNumber;

        // 🟩 Lo importante: asegurar persistencia
        mealPlanEntryRepository.Update(entry);
        trackingRepository.Update(tracking); // por consistencia del aggregate

        await unitOfWork.CompleteAsync();

        return tracking;
    }

}