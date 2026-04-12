using CommonRepositoryProject;
using DomainProject;

namespace CommonServiceProject;

public class CommonEntityService<T> : ICommonEntityService<T> where T : CommonEntity
{
    private readonly ICommonEntityRepository<T> _repository;

    protected CommonEntityService(ICommonEntityRepository<T> repository)
    {
        _repository = repository;
    }

    public virtual void Create(T entity)
    {
        _repository.Create(entity);
    }

    public virtual void Update(T entity)
    {
        _repository.Update(entity);
    }

    public virtual void Delete(T entity)
    {
        _repository.Delete(entity);
    }

    public virtual bool Exists(T entity)
    {
        return _repository.Exists(entity);
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAsync(id, cancellationToken);
    }

    public virtual async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }

    public virtual async Task<string?> GetContentAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _repository.GetContentAsync(id, cancellationToken);
    }
}