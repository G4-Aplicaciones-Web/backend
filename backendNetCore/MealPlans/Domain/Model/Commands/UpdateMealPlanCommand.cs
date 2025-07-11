namespace backendNetCore.MealPlans.Domain.Model.Commands;

public record UpdateMealPlanCommand(int Id, int ProfileId, string Summary, int Score);