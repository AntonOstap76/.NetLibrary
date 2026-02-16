using DomainProject;
namespace CommonRepositoryProject;

public interface ICommonCreatorRepository<T> where T : CommonCreator
{
    public void Add(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public Task<bool> SaveChangesAsync();
    public bool Exists(T entity);
    public Task<List<CommonEntity>> GetAllEntitiesAsync(Guid id);
}