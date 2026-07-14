using DomainProject;
using RepositoryToolsProject;

namespace CommonRepositoryProject;

public abstract class CommonEntityRepository<T> : ICommonEntityRepository<T>
    where T : CommonEntity
{
    private readonly IDbConnectionFactory _factory;
    
    protected abstract string TableName { get; }

    protected CommonEntityRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }

    public virtual async Task CreateAsync(T entity)
    {
        using var conn = _factory.CreateConnection();
        var sql = $"INSERT INTO {TableName} VALUES (@Id)";
        await conn.ExecuteAsync(sql, entity);
    }

    public virtual void Update(T entity)
    {
        var existing = _database.FirstOrDefault(e => e.Id == entity.Id);

        if (existing == null)
            throw new NotFoundException(typeof(T).Name, entity.Id);

        var index = _database.IndexOf(existing);
        _database[index] = entity;
    }

    public virtual void Delete(T entity)
    {
        var existing = _database.FirstOrDefault(e => e.Id == entity.Id);

        if (existing == null)
            throw new NotFoundException(typeof(T).Name, entity.Id);

        _database.RemoveAll(e => e.Id == entity.Id);
    }

    // public Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    // {
    //     return Task.FromResult(true);
    // }

    public virtual async Task<bool> Exists(T entity)
    {
        using var conn = _factory.CreateConnection();

        var parameters = new DynamicParameters();
        parameters.Add("p_table_name", typeof(T).Name);
        parameters.Add("p_id", entity.Id);

        return await conn.ExecuteScalarAsync<bool>("SELECT sp_entity_exists(@p_table_name, @p_id)",
            parameters);
    }

    public virtual Task<T?> GetAsync(Guid id, CancellationToken cancellationToken = default)
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

    public Task<Guid> GetContentsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
        //what we want to do here?
    }
}