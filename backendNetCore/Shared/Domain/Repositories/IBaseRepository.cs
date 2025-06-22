namespace backendNetCore.Shared.Domain.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Add an entity to the repository asynchronously.
    /// </summary>
    /// <param name="entity">The entity object to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddAsync(TEntity entity);
    
    /// <summary>
    /// Find an entity by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to find.</param>
    /// <returns>The entity object if found; otherwise, null.</returns>
    Task<TEntity?> FindByIdAsync(int id);
    
    /// <summary>
    /// Retrieve a list of all entities in the repository asynchronously.
    /// </summary>
    /// <returns>An enumerable collection of all entities in the repository.</returns>
    Task<IEnumerable<TEntity>> ListSync();
    
    /// <summary>
    /// Update an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity object to update.</param>
    void Update(TEntity entity);
    
    /// <summary>
    /// Remove an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity object to remove.</param>
    void Remove(TEntity entity);
}