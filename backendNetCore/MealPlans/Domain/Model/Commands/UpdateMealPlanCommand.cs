namespace backendNetCore.MealPlans.Domain.Model.Commands;

public record UpdateMealPlanCommand(int Id, string ProfileId, string Summary, int Score);