using backendNetCore.Shared.Domain.Repositories;
using backendNetCore.Tracking.Domain.Model.Entities;
using backendNetCore.Tracking.Domain.Model.ValueObjects;

namespace backendNetCore.Tracking.Domain.Repositories;

public interface IMealPlanTypeRepository : IBaseRepository<MealPlanType>
{
    Task<bool> ExistsByNameAsync(MealPlanTypes mealPlanType);
    Task<MealPlanType?> FindByNameAsync(MealPlanTypes name);
}