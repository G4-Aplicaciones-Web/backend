using backendNetCore.Profiles.Domain.Model.Entities;
using backendNetCore.Profiles.Domain.Model.Queries;
using backendNetCore.Profiles.Domain.Repositories;
using backendNetCore.Profiles.Domain.Services;

namespace backendNetCore.Profiles.Application.Internal.QueryServices;

public class ActivityLevelQueryService(IActivityLevelRepository activityLevelRepository) : IActivityLevelQueryService
{
    public async Task<ActivityLevel?> Handle(GetActivityLevelByIdQuery query)
    {
        return await activityLevelRepository.FindByIdAsync(query.ActivityLevelId);
    }

    public async Task<IEnumerable<ActivityLevel>> Handle(GetAllActivityLevelsQuery query)
    {
        return await activityLevelRepository.ListAsync();   
    }
}