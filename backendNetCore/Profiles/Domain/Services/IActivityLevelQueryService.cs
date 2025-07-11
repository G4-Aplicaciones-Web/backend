using backendNetCore.Profiles.Domain.Model.Entities;
using backendNetCore.Profiles.Domain.Model.Queries;

namespace backendNetCore.Profiles.Domain.Services;

public interface IActivityLevelQueryService
{
    Task<ActivityLevel?> Handle(GetActivityLevelByIdQuery query);
    Task<IEnumerable<ActivityLevel>> Handle(GetAllActivityLevelsQuery query);
}