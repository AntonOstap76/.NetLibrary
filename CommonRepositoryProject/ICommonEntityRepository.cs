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
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<string?> GetContentAsync(Guid id, CancellationToken cancellationToken = default);
}