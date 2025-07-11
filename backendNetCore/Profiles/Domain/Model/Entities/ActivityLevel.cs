namespace backendNetCore.Profiles.Domain.Model.Entities;

public class ActivityLevel
{
    /// <summary>
    /// Default constructor for the activity level entity
    /// </summary>
    public ActivityLevel()
    {
        Name = string.Empty;
        Description = string.Empty;
        ActivityFactor = 0;
    }
  
    /// <summary>
    /// Constructor for the activity level entity
    /// </summary>
    /// <param name="name">The name of the activity level</param>
    /// <param name="description">The description of the activity level</param>
    /// <param name="activityFactor">The activity factor (must be greater than 0)</param>
    /// <exception cref="ArgumentException">Thrown when activityFactor is not greater than 0</exception>
    public ActivityLevel(string name, string description, double activityFactor)
    {
        if (activityFactor <= 0)
            throw new ArgumentException("Activity factor must be greater than 0", nameof(activityFactor));
        
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        ActivityFactor = activityFactor;
    }

    
    public int Id { get; set; }
    public string Name { get; set;}
    public string Description { get; set;}
    public double ActivityFactor { get; set;}

    /// <summary>
    /// Calculates daily calories based on BMR and activity factor
    /// </summary>
    /// <param name="weight">Weight in kilograms</param>
    /// <param name="height">Height in centimeters</param>
    /// <param name="age">Age in years</param>
    /// <param name="isMale">True for male, false for female</param>
    /// <returns>Daily calorie needs</returns>
    public double CalculateCalories(double weight, double height, int age, bool isMale = true)
    {
        if (weight <= 0) throw new ArgumentException("Weight must be greater than 0", nameof(weight));
        if (height <= 0) throw new ArgumentException("Height must be greater than 0", nameof(height));
        if (age <= 0) throw new ArgumentException("Age must be greater than 0", nameof(age));
        
        // Mifflin-St Jeor Equation
        double bmr = 10 * weight + 6.25 * height - 5 * age + (isMale ? 5 : -161);
        return bmr * ActivityFactor;
    }

    
    /// <summary>
    /// Updates the activity level properties
    /// </summary>
    /// <param name="name">New name</param>
    /// <param name="description">New description</param>
    /// <param name="activityFactor">New activity factor</param>
    public void UpdateLevel(string name, string description, double activityFactor) 
    {
        if (activityFactor <= 0)
            throw new ArgumentException("Activity factor must be greater than 0", nameof(activityFactor));
            
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        ActivityFactor = activityFactor;
    }

}