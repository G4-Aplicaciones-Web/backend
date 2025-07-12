using backendNetCore.Profiles.Domain.Model.Aggregates;
using backendNetCore.Profiles.Domain.Repositories;
using backendNetCore.Shared.Infrastructure.Persistence.Configuration;
using backendNetCore.Shared.Infrastructure.Persistence.Repositories;

namespace backendNetCore.Profiles.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Profile repository implementation  
/// </summary>
/// <param name="context">
/// The database context
/// </param>
public class ProfileRepository(AppDbContext context) 
    : BaseRepository<Profile>(context), IProfileRepository
{

}