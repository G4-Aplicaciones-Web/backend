namespace backendNetCore.Profiles.Domain.Model.Entities;

public class Objective
{
    /// <summary>
    /// Default constructor for the objective entity
    /// </summary>
    public Objective()
    {
        Name = string.Empty;
        Score = 0;
    }
    
    /// <summary>
    /// Constructor for the objective entity
    /// </summary>
    /// <param name="name">The name of the objective</param>
    /// <param name="score">The score of the objective</param>
    /// <exception cref="ArgumentNullException">Thrown when name is null</exception>
    /// <exception cref="ArgumentException">Thrown when score is negative</exception>
    public Objective(string name, int score)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));
        if (score < 0)
            throw new ArgumentException("Score cannot be negative", nameof(score));
            
        Name = name;
        Score = score;
    }
    
    /// <summary>
    /// Unique identifier for the objective
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Name of the objective
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Score associated with the objective
    /// </summary>
    public int Score { get; set; }
    
    /// <summary>
    /// Updates the objective properties
    /// </summary>
    /// <param name="name">New name for the objective</param>
    /// <param name="score">New score for the objective</param>
    /// <exception cref="ArgumentException">Thrown when name is null/empty or score is negative</exception>
    public void UpdateObjective(string name, int score)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));
        if (score < 0)
            throw new ArgumentException("Score cannot be negative", nameof(score));
            
        Name = name;
        Score = score;
    }
    
    /// <summary>
    /// Increases the score by the specified amount
    /// </summary>
    /// <param name="points">Points to add to the current score</param>
    /// <exception cref="ArgumentException">Thrown when points is negative</exception>
    public void AddScore(int points)
    {
        if (points < 0)
            throw new ArgumentException("Points cannot be negative", nameof(points));
            
        Score += points;
    }
    
    /// <summary>
    /// Resets the score to zero
    /// </summary>
    public void ResetScore()
    {
        Score = 0;
    }
    
    /// <summary>
    /// Checks if the objective has achieved a minimum score
    /// </summary>
    /// <param name="minimumScore">The minimum score to check against</param>
    /// <returns>True if the current score meets or exceeds the minimum</returns>
    public bool HasAchievedMinimumScore(int minimumScore)
    {
        return Score >= minimumScore;
    }
}