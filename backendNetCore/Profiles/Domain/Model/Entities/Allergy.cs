namespace backendNetCore.Profiles.Domain.Model.Entities;

public class Allergy
{
    /// <summary>
    /// Default constructor for the Allergy entity
    /// </summary>
    public Allergy()
    {
        Name = string.Empty;
    }
    
    /// <summary>
    /// Constructor for the Allergy entity
    /// </summary>
    /// <param name="name">The name of the allergy</param>
    public Allergy(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Allergy name cannot be null or empty", nameof(name));
        
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }

}