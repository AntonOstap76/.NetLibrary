using CommonRepositoryProject;
using DomainProject;

namespace CommonServiceProject;

public class CommonCreatorService<T>  : ICommonCreatorService<T> where T : CommonCreator
{
    private readonly ICommonCreatorRepository<T> _repository;

    public CommonCreatorService(ICommonCreatorRepository<T> repository)
    {
        _repository = repository;
    }
    public void Add(T entity)
    {
       _repository.Add(entity);
    }

    public void Update(T entity)
    {
        _repository.Update(entity);
    }

    public void Delete(T entity)
    {
        _repository.Delete(entity);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _repository.SaveChangesAsync(cancellationToken);
    }

    public bool Exists(T entity)
    {
       return _repository.Exists(entity);
    }

    public async Task<List<CommonEntity>> GetAllEntitiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllEntitiesAsync(id, cancellationToken);
    }
}