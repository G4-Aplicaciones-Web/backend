using backendNetCore.Shared.Domain.Repositories;
using backendNetCore.Shared.Infrastructure.Persistence.Configuration;

namespace backendNetCore.Shared.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implements the Unit of Work pattern to manage transactions and ensure consistency in the data store.
/// </summary>
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;

    /// <summary>
    /// Saves all pending changes to the underlying data store asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}