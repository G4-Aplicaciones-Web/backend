namespace backendNetCore.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    /// <summary>
    /// Saves all pending changes to the underlying data store asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CompleteAsync();
}