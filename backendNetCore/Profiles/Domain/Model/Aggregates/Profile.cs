using backendNetCore.Profiles.Domain.Model.Commands;
using backendNetCore.Profiles.Domain.Model.Entities;
using backendNetCore.Profiles.Domain.Model.ValueObjects;

namespace backendNetCore.Profiles.Domain.Model.Aggregates;

/// <summary>
/// Profile Aggregate Root 
/// </summary>
/// <remarks>
/// This class represents the Profile aggregate root.
/// It contains the properties and methods to manage the profile information.
/// </remarks>
public partial class Profile
{
    public int Id { get; }

    public PersonName Name { get; private set; }

    public string FullName => Name.FullName;

    public string Gender { get; private set; }

    public double Height { get; private set; }

    public double Weight { get; private set; }

    public double Score { get; private set; }

    public ActivityLevel ActivityLevel { get; internal set; }

    public int ActivityLevelId { get; private set; }

    public Objective Objective { get; internal set; }

    public int ObjectivelId { get; private set; }

    private readonly List<Allergy> _allergies = new();
    public IReadOnlyCollection<Allergy> Allergies => _allergies.AsReadOnly();

    public Profile()
    {
        Name = new PersonName();
    }

    public Profile(string firstName, string lastName, string gender, double height, double weight, double score, int activityLevelId, int objectiveId) : this()
    {
        Name = new PersonName(firstName, lastName);
        Gender = gender;
        Height = height;
        Weight = weight;
        Score = score;
        ActivityLevelId = activityLevelId;
        ObjectivelId = objectiveId;
    }

    public Profile(CreateProfileCommand command)
        : this(command.FirstName, command.LastName, command.Gender, command.Height, command.Weight, command.Score, command.ActivityLevelId, command.ObjectiveId)
    {
    }

    public void AddAllergy(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return;
        if (_allergies.Any(a => a.Name == name.Trim().ToLowerInvariant())) return;

        _allergies.Add(new Allergy(name));
    }

    public void RemoveAllergy(string name)
    {
        var allergy = _allergies.FirstOrDefault(a => a.Name == name.Trim().ToLowerInvariant());
        if (allergy is not null)
        {
            _allergies.Remove(allergy);
        }
    }
    
    public void UpdateProfile(double height, double weight, int activityLevelId, int objectiveId)
    {
        if (height <= 0 || weight <= 0)
            throw new ArgumentException("Altura y peso deben ser mayores que cero");

        Height = height;
        Weight = weight;
        ActivityLevelId = activityLevelId;
        ObjectivelId = objectiveId;
    }

}
