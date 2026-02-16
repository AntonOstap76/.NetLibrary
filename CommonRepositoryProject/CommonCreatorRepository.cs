using DomainProject;

namespace CommonRepositoryProject;

public class CommonCreatorRepository<T> : ICommonCreatorRepository<T> 
    where T : CommonCreator
{
    private readonly List<T> _databaseCreator = new();
    public void Add(T entity)
    {
        _databaseCreator.Add(entity);
    }

    public void Update(T entity)
    {
       var existed =  _databaseCreator.FirstOrDefault(x => x.Id == entity.Id);
       if (existed == null) return;
       var index = _databaseCreator.IndexOf(existed);
       _databaseCreator[index] = entity;
    }

    public void Delete(T entity)
    {
        _databaseCreator.RemoveAll(e => e.Id == entity.Id);
    }

    public Task<bool> SaveChangesAsync()
    {
        return Task.FromResult(true);
    }

    public bool Exists(T entity)
    {
        return _databaseCreator.Any(e => e.Id == entity.Id);
    }

    public Task<List<CommonEntity>> GetAllEntitiesAsync(Guid id)
    {
        var creator = _databaseCreator.FirstOrDefault(c => c.Id == id);

        if (creator == null)
            return Task.FromResult(new List<CommonEntity>());

        return Task.FromResult(creator.Entities?.ToList() ?? new List<CommonEntity>());
    
    }
}