namespace backendNetCore.Profiles.Domain.Model.Commands;

public record CreateProfileCommand(
    string FirstName,
    string LastName,
    string Gender,
    double Height,
    double Weight,
    double Score,
    int ActivityLevelId,
    int ObjectiveId
);