using backendNetCore.Shared.Domain.Repositories;
using backendNetCore.Tracking.Domain.Model.Entities;

namespace backendNetCore.Tracking.Domain.Repositories;

public interface ITrackingMacronutrientRepository : IBaseRepository<TrackingMacronutrient>
{
    Task<bool> ExistsByValuesAsync(double calories, double carbs, double proteins, double fats);
}