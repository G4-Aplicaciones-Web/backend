using AlimentateplusPlatform.API.Shared.Domain.Repositories;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;
using AlimentateplusPlatform.API.Tracking.Domain.Model.ValueObjects;

namespace AlimentateplusPlatform.API.Tracking.Domain.Repositories;

public interface IMealPlanTypeRepository : IBaseRepository<MealPlanType>
{
    Task<bool> ExistsByNameAsync(MealPlanTypes mealPlanType);
    Task<MealPlanType?> FindByNameAsync(MealPlanTypes name);
}