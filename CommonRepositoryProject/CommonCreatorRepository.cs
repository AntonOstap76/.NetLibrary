using DomainProject;

namespace CommonRepositoryProject;

public class CommonCreatorRepository<T> : ICommonCreatorRepository<T>
    where T : CommonCreator
{
    public readonly List<T> _databaseCreator = new();

    public virtual void Add(T entity)
    {
        _databaseCreator.Add(entity);
    }

    public virtual void Update(T entity)
    {
        var existed = _databaseCreator.FirstOrDefault(x => x.Id == entity.Id);
        
        if (existed == null)
            throw new NotFoundException(typeof(T).Name, entity.Id);
        
        var index = _databaseCreator.IndexOf(existed);
        _databaseCreator[index] = entity;
    }

    public virtual void Delete(T entity)
    {
        var existed = _databaseCreator.FirstOrDefault(x => x.Id == entity.Id);

        if (existed == null)
            throw new NotFoundException(typeof(T).Name, entity.Id);

        _databaseCreator.RemoveAll(e => e.Id == entity.Id);
    }

    public virtual Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }

    public virtual bool Exists(T entity)
    {
        return _databaseCreator.Any(e => e.Id == entity.Id);
    }

    public virtual Task<List<CommonEntity>> GetAllEntitiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var creator = _databaseCreator.FirstOrDefault(c => c.Id == id);

        if (creator == null)
            return Task.FromResult(new List<CommonEntity>());

        return Task.FromResult(creator.Entities?.ToList() ?? new List<CommonEntity>());
    }
}