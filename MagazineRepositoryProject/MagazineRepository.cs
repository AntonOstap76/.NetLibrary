using System.Diagnostics;
using DomainProject;

namespace MagazineRepositoryProject;

public class MagazineRepository : IMagazineRepository
{
    public readonly List<Magazine> _database = new();

    public void Create(Magazine magazine)
    {
        _database.Add(magazine);
    }

    public void Update(Magazine magazine)
    {
        var existing = _database.FirstOrDefault(e => e.Id == magazine.Id);

        if (existing == null)
            throw new NotFoundException(nameof(Magazine), magazine.Id);

        var index = _database.IndexOf(existing);
        _database[index] = magazine;
    }

    public void Delete(Magazine magazine)
    {
        var existing = _database.FirstOrDefault(e => e.Id == magazine.Id);

        if (existing == null)
            throw new NotFoundException(nameof(Magazine), magazine.Id);

        _database.RemoveAll(e => e.Id == magazine.Id);
    }

    public bool Exists(Magazine magazine)
    {
        return _database.Any(e => e.Id == magazine.Id);
    }

    public Task<Magazine?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = _database.FirstOrDefault(i => i.Id == id);
        return Task.FromResult(entity);
    }

    public Task<List<Magazine>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = _database.ToList();
        return Task.FromResult(entities);
    }

    public Task<Magazine> GetMagazineAsync(Guid magazineIssueId, CancellationToken cancellationToken = default)
    {
        var entity = _database.FirstOrDefault(m => m.MagazineIssues.Any(i => i.Id == magazineIssueId));
        
        if (entity == null)
            throw new NotFoundException(nameof(MagazineIssue), magazineIssueId);

        return Task.FromResult(entity);
    }

    public Task<List<Magazine>> GetAllAsync(Guid publisherId, CancellationToken cancellationToken = default)
    {
        var entities = _database.FindAll(x => x.PublisherId.Id == publisherId);
        if (!entities.Any())
        {
            return Task.FromResult(new List<Magazine>());
        }

        return Task.FromResult(entities);
    }
}