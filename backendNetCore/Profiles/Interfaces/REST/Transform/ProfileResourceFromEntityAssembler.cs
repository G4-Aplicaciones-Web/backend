using backendNetCore.Profiles.Domain.Model.Aggregates;
using backendNetCore.Profiles.Interfaces.REST.Resources;
using backendNetCore.Recipes.Domain.Model.ValueObjects;

namespace backendNetCore.Profiles.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert Profile entity to ProfileResource 
/// </summary>
public static class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Profile entity)
    {
        return new ProfileResource(
            entity.Id, 
            entity.UserId.Value,
            entity.FullName, 
            entity.Gender,
            entity.Height,
            entity.Weight,
            entity.Score,
            ActivityLevelResourceFromEntityAssembler.ToResourceFromEntity(entity.ActivityLevel),
            ObjectiveResourceFromEntityAssembler.ToResourceFromEntity(entity.Objective)
            );
    }
}