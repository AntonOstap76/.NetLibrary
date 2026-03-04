using DomainProject;

namespace CommonServiceProject;

public interface ICommonCreatorService<T> where T : CommonCreator
{
    public void Add(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    public bool Exists(T entity);
    public Task<List<CommonEntity>> GetAllEntitiesAsync(Guid id, CancellationToken cancellationToken = default);
}