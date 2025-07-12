namespace backendNetCore.Profiles.Interfaces.REST.Resources;

public record ProfileResource(
    int Id, 
    int UserId,
    string FullName,
    string Gender, 
    double Height,
    double Weight,
    double Score,
    ActivityLevelResource ActivityLevel,
    ObjectiveResource Objective
    );