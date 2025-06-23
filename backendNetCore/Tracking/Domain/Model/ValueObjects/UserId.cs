namespace backendNetCore.Tracking.Domain.Model.ValueObjects;

public record UserId
{
    public int Id { get; init; }

    protected UserId() { }
    public UserId(int id)
    {
        if (id <= 0)
            throw new ArgumentException("UserId must exist");

        Id = id;
    }
}
