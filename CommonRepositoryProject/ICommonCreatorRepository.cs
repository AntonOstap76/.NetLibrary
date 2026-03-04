using DomainProject;
namespace CommonRepositoryProject;

public interface ICommonCreatorRepository<T> where T : CommonCreator
{
    public void Add(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    public bool Exists(T entity);
    
    /// <summary>
    /// Get all entities regardless their types
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<CommonEntity>> GetAllEntitiesAsync(Guid id, CancellationToken cancellationToken = default);
}