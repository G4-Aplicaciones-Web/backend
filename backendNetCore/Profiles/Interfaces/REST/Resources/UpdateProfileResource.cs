namespace backendNetCore.Profiles.Interfaces.REST.Resources;

public record UpdateProfileResource(
    double Height,
    double Weight,
    int ObjectiveId,
    int ActivityLevelId
    );