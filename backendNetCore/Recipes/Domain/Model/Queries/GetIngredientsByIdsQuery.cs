namespace backendNetCore.Recipes.Domain.Model.Queries;

public record GetIngredientsByIdsQuery(IEnumerable<int> Ids);