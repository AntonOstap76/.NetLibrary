using DomainProject;

namespace CommonRepositoryProject;

public class CommonEntityRepository<T> : ICommonEntityRepository<T>
    where T : CommonEntity
{
    private readonly List<T> _database = new();

    public virtual void Create(T entity)
    {
        _database.Add(entity);
    }

    public virtual void Update(T entity)
    {
        var existing = _database.FirstOrDefault(e => e.Id == entity.Id);

        if (existing == null)
            return;

        var index = _database.IndexOf(existing);
        _database[index] = entity;
    }

    public virtual void Delete(T entity)
    {
        _database.RemoveAll(e => e.Id == entity.Id);
    }

    // public Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    // {
    //     return Task.FromResult(true);
    // }

    public virtual bool Exists(T entity)
    {
        return _database.Any(e => e.Id == entity.Id);
    }

    public virtual Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = _database.FirstOrDefault(i => i.Id == id);
        return Task.FromResult(entity);
        
    }

    public virtual Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = _database.ToList();
        return Task.FromResult(entities);
    }

    public virtual Task<string?> GetContentAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = _database.FirstOrDefault(e => e.Id == id);

        if (entity == null)
            return Task.FromResult<string>(null);

        var contentProperty = typeof(T).GetProperty("Content");

        if (contentProperty == null)
            return Task.FromResult<string>(null);

        var content = contentProperty.GetValue(entity)?.ToString();

        return Task.FromResult(content);
    }
}