using backendNetCore.MealPlans.Domain.Model.Aggregates;
using backendNetCore.Shared.Domain.Repositories;

namespace backendNetCore.MealPlans.Domain.Repositories;

public interface IMealPlanRepository : IBaseRepository<MealPlan>
{


    Task<IEnumerable<MealPlan>> FindByProfileIdAsync(int profileId);

    Task<MealPlan?> FindByProfileIdAndScoreAsync(int profileId, int score);

}