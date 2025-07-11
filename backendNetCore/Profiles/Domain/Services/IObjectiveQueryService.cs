using backendNetCore.Profiles.Domain.Model.Entities;
using backendNetCore.Profiles.Domain.Model.Queries;

namespace backendNetCore.Profiles.Domain.Services;

public interface IObjectiveQueryService
{
    Task<IEnumerable<Objective>> Handle(GetAllObjectivesQuery query);
    Task<Objective?> Handle(GetObjectiveByIdQuery query);
}