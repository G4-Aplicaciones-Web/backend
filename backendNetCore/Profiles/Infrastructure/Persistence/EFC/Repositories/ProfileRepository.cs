using backendNetCore.Profiles.Domain.Model.Aggregates;
using backendNetCore.Profiles.Domain.Model.ValueObjects;
using backendNetCore.Profiles.Domain.Repositories;
using backendNetCore.Shared.Infrastructure.Persistence.Configuration;
using backendNetCore.Shared.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

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
    public new async Task<IEnumerable<Profile>> ListAsync()
    {
        return await Context.Set<Profile>()
            .Include(p => p.ActivityLevel)
            .Include(p => p.Objective)
            .ToListAsync();
    }

    public new async Task<Profile?> FindByIdAsync(int id)
    {
        return await Context.Set<Profile>()
            .Include(p => p.ActivityLevel)
            .Include(p => p.Objective)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Profile?> FindByUserIdAsync(UserId userId)
    {
        return await Context.Set<Profile>()
            .Include(p => p.ActivityLevel)
            .Include(p => p.Objective)
            .Include("_allergies")
            .FirstOrDefaultAsync(p => p.UserId == userId);
    }
}