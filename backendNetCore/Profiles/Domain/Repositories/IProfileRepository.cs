using backendNetCore.Profiles.Domain.Model.Aggregates;
using backendNetCore.Profiles.Domain.Model.ValueObjects;
using backendNetCore.Shared.Domain.Repositories;

namespace backendNetCore.Profiles.Domain.Repositories;

/// <summary>
/// Profile repository interface 
/// </summary>
public interface IProfileRepository : IBaseRepository<Profile>
{
    
}