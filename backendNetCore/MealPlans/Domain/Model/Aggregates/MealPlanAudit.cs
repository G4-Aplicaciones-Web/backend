using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace backendNetCore.MealPlans.Domain.Model.Aggregates;

public partial class MealPlan : IEntityWithCreatedUpdatedDate
{
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
}