namespace backendNetCore.Tracking.Domain.Model.ValueObjects;


public class RecipeId
{
    public int Id { get; }

    public RecipeId(int id)
    {
        if (id <= 0) throw new ArgumentException("Recipe id must Exists");
        Id = id;
    }

    // EF Core lo necesita para materializar entidades desde la base de datos
    protected RecipeId() { }
}