using backendNetCore.MealPlans.Domain.Model.Aggregates;
using backendNetCore.MealPlans.Domain.Model.Queries;
using backendNetCore.MealPlans.Domain.Repositories;
using backendNetCore.MealPlans.Domain.Services;
using backendNetCore.Shared.Domain.Repositories;

namespace backendNetCore.MealPlans.Application.Internal.QueryServices;

public class MealPlanQueryService(IMealPlanRepository mealPlanRepository) 
: IMealPlanQueryService
{
    public async Task<IEnumerable<MealPlan>> Handle(GetAllMealPlanByProfileIdQuery query)
    {
        return await mealPlanRepository.FindByProfileIdAsync(query.ProfileId);
    }

    public async Task<MealPlan?> Handle(GetMealPlanByIdQuery query)
    {
        return await mealPlanRepository.FindByIdAsync(query.Id);
    }

    public async Task<MealPlan?> Handle(GetMealPlanByProfileIdAndScoreQuery query)
    {
        return await mealPlanRepository.FindByProfileIdAndScoreAsync(query.ProfileId, query.Score);
    }

    public async Task<IEnumerable<MealPlan>> Handle(GetAllMealPlansQuery query)
    {
        return await mealPlanRepository.ListAsync();
    }
}