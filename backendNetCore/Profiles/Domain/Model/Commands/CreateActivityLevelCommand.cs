namespace backendNetCore.Profiles.Domain.Model.Commands;

public record CreateActivityLevelCommand(string Name, string Description, double ActivityFactor);