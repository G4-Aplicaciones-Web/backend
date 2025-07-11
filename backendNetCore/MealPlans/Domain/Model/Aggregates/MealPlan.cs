using backendNetCore.MealPlans.Domain.Model.Commands;

namespace backendNetCore.MealPlans.Domain.Model.Aggregates;

public partial class MealPlan
{
    public int Id { get; set; }
    
    public int ProfileId { get; set; }
    
    public string Summary { get; set; } 
    
    public int Score { get; set; }

    protected MealPlan()
    {
        ProfileId = 0;
        Summary = string.Empty;
        Score = 0;
    }
    
    /// <summary>
    /// Constructor for the meal plan aggregate
    /// </summary>
    /// <remarks>
    /// The constructor is the command handler for the CreateMealPlanCommand
    /// </remarks>
    /// <param name="command"></param>

    public MealPlan(CreateMealPlanCommand command)
    {
        ProfileId = command.ProfileId;
        Summary = command.Summary;
        Score = command.Score;
    }
    
    /// <summary>
    /// Updates the meal plan with new data
    /// </summary>
    /// <param name="command">The update command containing new data</param>
    public void Update(UpdateMealPlanCommand command)
    {
        ProfileId = command.ProfileId;
        Summary = command.Summary;
        Score = command.Score;
    }
}