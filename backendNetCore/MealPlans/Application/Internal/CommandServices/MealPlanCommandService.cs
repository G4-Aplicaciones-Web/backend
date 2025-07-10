using backendNetCore.MealPlans.Domain.Model.Aggregates;
using backendNetCore.MealPlans.Domain.Model.Commands;
using backendNetCore.MealPlans.Domain.Repositories;
using backendNetCore.MealPlans.Domain.Services;
using backendNetCore.Shared.Domain.Repositories;

namespace backendNetCore.MealPlans.Application.Internal.CommandServices;

/// <summary>
/// Meal Plan command servicd
/// <remarks>
/// This class implements the basic operations for meal plans
/// </remarks>
/// </summary>
/// <param name="mealPlanRepository"> the instance of mealPlanRepository</param>
/// <param name="unitOfWork"> the instance of unitOfWork</param>
public class MealPlanCommandService(IMealPlanRepository mealPlanRepository,
    IUnitOfWork unitOfWork) : IMealPlanCommandService
{
    public async Task<MealPlan?> Handle(CreateMealPlanCommand command)
    {
        var mealPlan =
            await mealPlanRepository.FindByProfileIdAndScoreAsync(command.ProfileId, command.Score);
        if (mealPlan != null)
            throw new Exception("Meal plan already exists");
        mealPlan = new MealPlan(command);
        try
        {
            await mealPlanRepository.AddAsync(mealPlan);
            await unitOfWork.CompleteAsync();

        } catch (Exception e)
        {
            return null;
        }

        return mealPlan;
    }

    public async Task<MealPlan?> Handle(UpdateMealPlanCommand command)
    {
        var mealPlan = await mealPlanRepository.FindByIdAsync(command.Id);
        if (mealPlan == null) return null;
        
        mealPlan.Update(command);
        
        try
        {
            mealPlanRepository.Update(mealPlan);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return null;
        }

        return mealPlan;
    }

    public async Task<bool> Handle(DeleteMealPlanCommand command)
    {
        var mealPlan = await mealPlanRepository.FindByIdAsync(command.Id);
        if (mealPlan == null) return false;
        
        try
        {
            mealPlanRepository.Remove(mealPlan);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}