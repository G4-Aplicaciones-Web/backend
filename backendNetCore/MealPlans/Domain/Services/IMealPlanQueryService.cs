using backendNetCore.MealPlans.Domain.Model.Aggregates;
using backendNetCore.MealPlans.Domain.Model.Queries;

namespace backendNetCore.MealPlans.Domain.Services;

/// <summary>
/// Interface for the MealPlanQueryService
/// </summary>
/// <remarks>
/// This interface defines the basic operations for the MealPlanQueryService
/// </remarks>
public interface IMealPlanQueryService
{
    /// <summary>
    /// Handle the GetAllMealPlanByProfileId
    /// </summary>
    /// <remarks>
    /// This method handles the GetAllMealPlansByProfileId. It returns all
    /// the meal plans for the given ProductId
    /// </remarks>
    /// <param name="query">The GetAllMealPlanByProfileId</param>
    /// <returns> An IEnumerable containing the mealplans objects</returns>
    Task<IEnumerable<MealPlan>> Handle(GetAllMealPlanByProfileIdQuery query);
    
    
    /// <summary>
    /// Handle the GetMealPlanByIdQuery
    /// </summary>
    /// <remarks>
    /// This method handles the GetMealPlanByIdQuery. it returns the
    /// meal plan for given id.
    /// </remarks>
    /// <param name="query">The GetMealPlanById query</param>
    /// <returns> The meal plan object if found, or null otherwise</returns>

    Task<MealPlan?> Handle(GetMealPlanByIdQuery query);
    
    
    /// <summary>
    /// Handle the GetMealPlanByProfileIdAndScoreQuery
    /// </summary>
    /// <remarks>
    /// This method handles the GetMealPlanByProfileIdAndScoreQuery. It returns the
    /// meal plan for the ProfileId and Score.
    /// <param name="query">The GetMealPlanByProfileIdAndScoreQuery query </param>
    /// <returns> The meal plan object if found, or null otherwise</returns>

    Task<MealPlan?> Handle(GetMealPlanByProfileIdAndScoreQuery query);
    
    /// <summary>
    /// Handle the GetAllMealPlansQuery
    /// </summary>
    /// <remarks>
    /// This method handles the GetAllMealPlansQuery. It returns all
    /// meal plans in the system.
    /// </remarks>
    /// <param name="query">The GetAllMealPlansQuery</param>
    /// <returns>An IEnumerable containing all meal plan objects</returns>
    Task<IEnumerable<MealPlan>> Handle(GetAllMealPlansQuery query);
}