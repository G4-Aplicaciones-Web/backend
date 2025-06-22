using backendNetCore.Recipes.Domain.Model.Entities;
using backendNetCore.Recipes.Domain.Model.ValueObjects;

namespace backendNetCore.Recipes.Domain.Model.Aggregates;

public partial class Recipe 
{
    public int Id { get; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string UrlInstructions { get; private set; }
    public MacronutrientValues TotalNutrients { get; private set; }
    public UserId UserId { get; private set; }
    private List<IngredientQuantity> _ingredients;
    public IReadOnlyCollection<IngredientQuantity> Ingredients => _ingredients.AsReadOnly();

    public ERecipeType RecipeType { get; private set; }
    
    protected Recipe()
    {
        _ingredients = new List<IngredientQuantity>();
        RecipeType = ERecipeType.NoRecipeType;
        TotalNutrients = MacronutrientValues.Empty;
        Name = string.Empty;
        Description = string.Empty;
        UrlInstructions = string.Empty;
        UserId = new UserId(0);
    }

    public Recipe(string name, string description, UserId userId, ERecipeType recipeType = ERecipeType.NoRecipeType, string urlInstructions = "")
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Recipe name cannot be null or empty", nameof(name));
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Recipe description cannot be null or empty", nameof(description));
        
        Name = name;
        Description = description;
        UrlInstructions = urlInstructions;
        UserId = userId ?? throw new ArgumentNullException(nameof(userId));
        RecipeType = recipeType;
        _ingredients = new List<IngredientQuantity>();
        TotalNutrients = MacronutrientValues.Empty;
    }

    
    public void AddIngredient(Ingredient ingredient, double quantity)
    {
        if (ingredient == null)
            throw new ArgumentNullException(nameof(ingredient));
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));

        var existingIngredient = _ingredients.FirstOrDefault(iq => iq.IngredientId == ingredient.Id);

        if (existingIngredient != null)
        {
            existingIngredient.UpdateQuantity(quantity);
        }
        else
        {
            _ingredients.Add(new IngredientQuantity(ingredient.Id, quantity));
        }
    }
    
    public void RemoveIngredient(int ingredientId)
    {
        var ingredientToRemove = _ingredients.FirstOrDefault(iq => iq.IngredientId == ingredientId);
        
        if (ingredientToRemove == null)
            throw new InvalidOperationException($"Ingredient with ID {ingredientId} not found in recipe");

        _ingredients.Remove(ingredientToRemove);
    }
    
    public void UpdateIngredientQuantity(int ingredientId, double newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(newQuantity));

        var ingredientQuantity = _ingredients.FirstOrDefault(iq => iq.IngredientId == ingredientId);

        if (ingredientQuantity == null)
            throw new InvalidOperationException($"Ingredient with ID {ingredientId} not found in recipe");

        ingredientQuantity.UpdateQuantity(newQuantity);
    }

    
    public void UpdateDetails(string name, string description, ERecipeType recipeType, string urlInstructions = "")
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Recipe name cannot be null or empty", nameof(name));
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Recipe description cannot be null or empty", nameof(description));

        Name = name;
        Description = description;
        RecipeType = recipeType;
        UrlInstructions = urlInstructions;
    }
    
    public void RecalculateTotalNutrients(IEnumerable<Ingredient> allIngredientsInRecipe)
    {
        if (allIngredientsInRecipe == null)
            throw new ArgumentNullException(nameof(allIngredientsInRecipe));

        if (!_ingredients.Any())
        {
            TotalNutrients = MacronutrientValues.Empty;
            return;
        }

        // Materializar la colección una sola vez en un Dictionary para acceso O(1)
        var ingredientsList = allIngredientsInRecipe.ToList();
        var ingredientsLookup = ingredientsList.ToDictionary(i => i.Id, i => i);
    
        var totalNutrients = MacronutrientValues.Empty;

        foreach (var ingredientQuantity in _ingredients)
        {
            // Acceso O(1) en lugar de búsqueda lineal O(n)
            if (ingredientsLookup.TryGetValue(ingredientQuantity.IngredientId, out var ingredient))
            {
                if (ingredient?.Nutrients != null)
                {
                    var nutrientsForQuantity = ingredient.Nutrients.CalculateForQuantity(ingredientQuantity.Quantity);
                    totalNutrients = totalNutrients.Add(nutrientsForQuantity);
                }
            }
        }

        TotalNutrients = totalNutrients;
    }
    
    public bool IsValid()
    {
        return _ingredients.Any() && 
               !string.IsNullOrWhiteSpace(Name) && 
               !string.IsNullOrWhiteSpace(Description);
    }
    
    public bool ContainsIngredient(int ingredientId)
    {
        return _ingredients.Any(iq => iq.IngredientId == ingredientId);
    }
    
    public double? GetIngredientQuantity(int ingredientId)
    {
        return _ingredients.FirstOrDefault(iq => iq.IngredientId == ingredientId)?.Quantity;
    }
}
