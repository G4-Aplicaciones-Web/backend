namespace backendNetCore.Profiles.Domain.Model.Commands;

public record AddAllergyToProfileCommand(
    int ProfileId,
    string AllergyName
);