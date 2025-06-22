using backendNetCore.Recipes.Domain.Model.ValueObjects;

namespace backendNetCore.Recipes.Domain.Model.Aggregates;

public class Ingredient
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public MacronutrientValues Nutrients { get; private set; }
    public ECategory IngredientCategory { get; private set; }

    protected Ingredient()
    {
        IngredientCategory = ECategory.NoCategory;
        Nutrients = new MacronutrientValues(0, 0, 0, 0);
    }
    
    public Ingredient(string name, MacronutrientValues nutrients, ECategory category = ECategory.NoCategory)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Ingredient name cannot be null or empty", nameof(name));
        
        Name = name;
        Nutrients = nutrients ?? throw new ArgumentNullException(nameof(nutrients));
        IngredientCategory = category;
    }
    
    public void UpdateNutrients(MacronutrientValues newNutrients)
    {
        Nutrients = newNutrients ?? throw new ArgumentNullException(nameof(newNutrients));
    }

    public void UpdateCategory(ECategory newCategory)
    {
        IngredientCategory = newCategory;
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Ingredient name cannot be null or empty", nameof(newName));
        
        Name = newName;
    }

    public void UpdateDetails(string name, MacronutrientValues nutrients, ECategory category)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Ingredient name cannot be null or empty", nameof(name));
        
        Name = name;
        Nutrients = nutrients ?? throw new ArgumentNullException(nameof(nutrients));
        IngredientCategory = category;
    }
    
    public double GetCaloriesPerGram()
    {
        return Nutrients.Calories / 100.0;
    }
}
