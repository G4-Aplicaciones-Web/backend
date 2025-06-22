using backendNetCore.Recipes.Domain.Model.ValueObjects;

namespace backendNetCore.Recipes.Domain.Model.Queries;

public record GetIngredientsByCategoryQuery(ECategory Category);