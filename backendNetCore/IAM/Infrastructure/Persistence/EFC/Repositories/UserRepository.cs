using backendNetCore.IAM.Domain.Model.Aggregates;
using backendNetCore.IAM.Domain.Repositories;
using backendNetCore.Shared.Infrastructure.Persistence.Configuration;
using backendNetCore.Shared.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backendNetCore.IAM.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// User repository implementation. 
/// </summary>
/// <param name="context">
/// The <see cref="AppDbContext"/> database context.
/// </param>
public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    // inheritedDoc
    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.Username.Equals(username));
    }

    // inheritedDoc
    public bool ExistsByUsername(string username)
    {
        return Context.Set<User>().Any(user => user.Username.Equals(username));
    }
}