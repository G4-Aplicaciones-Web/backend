using AlimentateplusPlatform.API.Tracking.Domain.Model.ValueObjects;

namespace AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;

public class MealPlanType
{
    public long Id { get; set; }
    public MealPlanTypes Name { get; set; }

    // Constructor sin parámetros 
    public MealPlanType() { }

    // Constructor con todos los parámetros 
    public MealPlanType(long id, MealPlanTypes name)
    {
        Id = id;
        Name = name;
    }

    // Constructor personalizado
    public MealPlanType(MealPlanTypes name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return Name.ToString();
    }

    public static MealPlanType GetDefaultMealPlanType()
    {
        return new MealPlanType(MealPlanTypes.Healthy);
    }
}