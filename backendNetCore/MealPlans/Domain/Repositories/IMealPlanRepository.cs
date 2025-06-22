using backendNetCore.MealPlans.Domain.Model.Aggregates;
using backendNetCore.Shared.Domain.Repositories;

namespace backendNetCore.MealPlans.Domain.Repositories;

public interface IMealPlanRepository : IBaseRepository<MealPlan>
{


    Task<IEnumerable<MealPlan>> FindByProfileIdAsync(string profileId);

    Task<MealPlan?> FindByProfileIdAndScoreAsync(string profileId, int score);

}