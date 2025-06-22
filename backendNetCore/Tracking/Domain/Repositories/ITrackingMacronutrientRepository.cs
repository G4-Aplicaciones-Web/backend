using AlimentateplusPlatform.API.Shared.Domain.Repositories;
using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;

namespace AlimentateplusPlatform.API.Tracking.Domain.Repositories;

public interface ITrackingMacronutrientRepository : IBaseRepository<TrackingMacronutrient>
{
    Task<bool> ExistsByValuesAsync(double calories, double carbs, double proteins, double fats);
}