namespace backendNetCore.Profiles.Domain.Model.Commands;

public record RemoveAllergyFromProfileCommand(
    int ProfileId,
    string AllergyName
);