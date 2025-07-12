using backendNetCore.IAM.Interfaces.ACL;
using backendNetCore.Profiles.Interfaces.ACL;
using backendNetCore.Tracking.Domain.Model.Commands;
using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Model.ValueObjects;
using backendNetCore.Tracking.Domain.Services;

namespace backendNetCore.Tracking.Application.Internal.OutBoundServices.ACL;

/// <summary>
/// External service to interact with Profiles bounded context
/// Acts as an Anti-Corruption Layer (ACL) between Tracking and Profiles contexts
/// </summary>
/// <summary>
/// External service to interact with Profiles and IAM bounded contexts
/// Acts as an Anti-Corruption Layer (ACL) between Tracking, Profiles, and IAM contexts
/// </summary>
public class ExternalProfileService : IExternalProfileService
{
    private readonly IProfilesContextFacade _profilesContextFacade;
    private readonly IIamContextFacade _iamContextFacade;
    private readonly ITrackingGoalCommandService _trackingGoalCommandService;

    public ExternalProfileService(
        IProfilesContextFacade profilesContextFacade,
        IIamContextFacade iamContextFacade,
        ITrackingGoalCommandService trackingGoalCommandService)
    {
        _profilesContextFacade = profilesContextFacade;
        _iamContextFacade = iamContextFacade;
        _trackingGoalCommandService = trackingGoalCommandService;
    }

    public async Task<bool> ExistsProfileById(int profileId)
    {
        return await _profilesContextFacade.ExistsProfileById(profileId);
    }

    public async Task<string?> GetObjectiveNameByProfileId(int profileId)
    {
        return await _profilesContextFacade.FetchObjectiveNameByProfileId(profileId);
    }

    public async Task<bool> ExistsUserById(int userId)
    {
        // Validar que el userId existe en el contexto IAM
        var username = await _iamContextFacade.FetchUsernameByUserId(userId);
        return !string.IsNullOrEmpty(username);
    }

    public async Task<TrackingGoal?> CreateTrackingGoalBasedOnProfile(int profileId, int userId)
    {
        // 1. Verificar que el usuario existe en el contexto IAM
        var userExists = await ExistsUserById(userId);
        if (!userExists)
        {
            throw new InvalidOperationException($"User with ID {userId} does not exist in IAM context");
        }

        // 2. Verificar que el perfil existe
        var profileExists = await _profilesContextFacade.ExistsProfileById(profileId);
        if (!profileExists)
        {
            throw new InvalidOperationException($"Profile with ID {profileId} does not exist");
        }

        // 3. Obtener el objetivo del perfil
        var objectiveName = await _profilesContextFacade.FetchObjectiveNameByProfileId(profileId);
        if (string.IsNullOrEmpty(objectiveName))
        {
            throw new InvalidOperationException($"No objective found for profile {profileId}");
        }

        // 4. Mapear el objetivo del perfil a GoalType DisplayName
        var goalTypeDisplayName = MapObjectiveToGoalTypeDisplayName(objectiveName);
        
        // 5. Crear el comando con el userId validado y el GoalType del perfil
        var command = new CreateTrackingGoalByObjectiveCommand(new UserId(userId), goalTypeDisplayName);
        
        // 6. Ejecutar el comando a través del servicio
        return await _trackingGoalCommandService.Handle(command);
    }

    /// <summary>
    /// Maps the objective name from Profiles context to a GoalType DisplayName in Tracking context
    /// This acts as a translation layer between the two bounded contexts
    /// </summary>
    /// <param name="objectiveName">The objective name from Profiles context</param>
    /// <returns>The corresponding GoalType DisplayName</returns>
    private string MapObjectiveToGoalTypeDisplayName(string objectiveName)
    {
        // Normalizar el nombre del objetivo para comparación
        var normalizedObjective = objectiveName.Trim().ToLowerInvariant();
        
        return normalizedObjective switch
        {
            // Mapeo directo basado en los GoalType existentes
            "mantenimiento" => GoalType.Mantenimiento.DisplayName,
            "perdida peso" or "pérdida peso" or "perdida de peso" or "pérdida de peso" or "perder peso" => GoalType.PerdidaPeso.DisplayName,
            "ganancia muscular" or "ganar músculo" or "ganar musculo" or "aumentar masa muscular" => GoalType.GananciaMuscular.DisplayName,
            
            // Mapeos adicionales para variaciones comunes
            "mantener peso" or "mantener" => GoalType.Mantenimiento.DisplayName,
            "bajar peso" or "adelgazar" or "reducir peso" => GoalType.PerdidaPeso.DisplayName,
            "aumentar masa" or "bulking" or "ganar peso" => GoalType.GananciaMuscular.DisplayName,
            
            // Si no encuentra coincidencia, lanza excepción
            _ => throw new InvalidOperationException($"No se pudo mapear el objetivo '{objectiveName}' a un GoalType válido. Objetivos válidos: {string.Join(", ", GoalType.GetAll().Select(g => g.DisplayName))}")
        };
    }
}
