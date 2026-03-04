using DomainProject;
namespace CommonRepositoryProject;

public interface ICommonEntityRepository<T> where T : CommonEntity
{
    //create async
    public void Create(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    // public Task<bool>SaveChangesAsync(CancellationToken cancellationToken = default);
    public bool Exists(T entity);
    public Task<T?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<string?> GetContentAsync(Guid id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Asmetric API
    /// </summary>
    /// <param name="ids">Entities ids for e.g books</param>
    /// <param name="cancellationToken"></param>
    /// <returns> request id for user</returns>
    public Task<Guid> GetContentsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default); 
}