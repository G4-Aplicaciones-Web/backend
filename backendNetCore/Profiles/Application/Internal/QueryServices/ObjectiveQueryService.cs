using backendNetCore.Profiles.Domain.Model.Entities;
using backendNetCore.Profiles.Domain.Model.Queries;
using backendNetCore.Profiles.Domain.Repositories;
using backendNetCore.Profiles.Domain.Services;

namespace backendNetCore.Profiles.Application.Internal.QueryServices;

public class ObjectiveQueryService(IObjectiveRepository objectiveRepository) : IObjectiveQueryService
{
    public async Task<Objective?> Handle(GetObjectiveByIdQuery query)
    {
        return await objectiveRepository.FindByIdAsync(query.ObjectiveId);
    }

    public async Task<IEnumerable<Objective>> Handle(GetAllObjectivesQuery query)
    {
        return await objectiveRepository.ListAsync();   
    }
}