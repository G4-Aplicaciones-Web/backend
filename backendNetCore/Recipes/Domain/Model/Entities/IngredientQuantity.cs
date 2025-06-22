namespace backendNetCore.Recipes.Domain.Model.Entities;

public class IngredientQuantity
{
    public int Id { get; private set; }
    public int IngredientId { get; private set; }
    public double Quantity { get; private set; } // Cantidad en gramos
    
    // Constructor para EF Core
    protected IngredientQuantity() { }
    
    // Constructor principal
    public IngredientQuantity(int ingredientId, double quantity)
    {
        if (ingredientId <= 0)
            throw new ArgumentException("IngredientId must be positive", nameof(ingredientId));
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));
        
        IngredientId = ingredientId;
        Quantity = quantity;
    }

    // Métodos de comportamiento
    public void UpdateQuantity(double newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(newQuantity));
        
        Quantity = newQuantity;
    }

    // Método para convertir unidades (si es necesario en el futuro)
    public double GetQuantityInUnit(MeasurementUnit unit)
    {
        return unit switch
        {
            MeasurementUnit.Grams => Quantity,
            MeasurementUnit.Kilograms => Quantity / 1000.0,
            MeasurementUnit.Ounces => Quantity * 0.035274,
            MeasurementUnit.Pounds => Quantity * 0.00220462,
            _ => Quantity
        };
    }

    // Método para validar la cantidad
    public bool IsValidQuantity()
    {
        return Quantity > 0 && Quantity <= 10000; // Máximo 10kg por ingrediente
    }
}

// Enum para unidades de medida (extensible)
public enum MeasurementUnit
{
    Grams,
    Kilograms,
    Ounces,
    Pounds,
    Milliliters,
    Liters,
    Cups,
    Tablespoons,
    Teaspoons
}