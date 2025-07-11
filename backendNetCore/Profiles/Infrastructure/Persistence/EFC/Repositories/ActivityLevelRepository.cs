using backendNetCore.Profiles.Domain.Model.Entities;
using backendNetCore.Shared.Infrastructure.Persistence.Configuration;
using backendNetCore.Shared.Infrastructure.Persistence.Repositories;

namespace backendNetCore.Profiles.Infrastructure.Persistence.EFC.Repositories;

public class ActivityLevelRepository(AppDbContext context) : BaseRepository<ActivityLevel>(context)
{
    
}