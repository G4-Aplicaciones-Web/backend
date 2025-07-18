using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace backendNetCore.Recommendations.Domain.Model.Aggregates;

public partial class Recommendation : IEntityWithCreatedUpdatedDate
{
    [Column("CreatedAt")]
    public DateTimeOffset? CreatedDate { get; set; }

    [Column("UpdatedAt")]
    public DateTimeOffset? UpdatedDate { get; set; }
}