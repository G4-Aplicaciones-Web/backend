namespace backendNetCore.Profiles.Interfaces.REST.Resources;

public record CreateProfileResource(
    string FirstName, 
    string LastName, 
    string Gender, 
    double Height, 
    double Weight, 
    double Score, 
    int ActivityLevelId, 
    int ObjectiveId
    );