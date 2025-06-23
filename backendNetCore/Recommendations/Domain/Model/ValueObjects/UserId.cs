namespace backendNetCore.Recommendations.Domain.Model.ValueObjects;

public record UserId(long Value)
{
    public UserId() : this(0) {}

    public override string ToString() => Value.ToString();
}