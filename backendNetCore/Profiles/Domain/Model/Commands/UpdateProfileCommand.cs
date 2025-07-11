namespace backendNetCore.Profiles.Domain.Model.Commands;

public record UpdateProfileCommand(
    int ProfileId,
    double Height,
    double Weight,
    int ObjectiveId,
    int ActivityLevelId
);