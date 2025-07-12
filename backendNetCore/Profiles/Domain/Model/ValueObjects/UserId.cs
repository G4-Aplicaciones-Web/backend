namespace backendNetCore.Profiles.Domain.Model.ValueObjects;

public record UserId
{
    public int Value { get; }

    public UserId(int value)
    {
        if (value <= 0)
            throw new ArgumentException("UserId must be a positive integer", nameof(value));

        Value = value;
    }

    // Constructor sin parámetros para EF Core
    private UserId() => Value = 0;
}