using DomainProject;

namespace MagazineRepositoryProject;

public interface IMagazineRepository
{
    //create async
    public void Create(Magazine magazine);
    public void Update(Magazine magazine);
    public void Delete(Magazine magazine);
    // public Task<bool>SaveChangesAsync(CancellationToken cancellationToken = default);
    public bool Exists(Magazine magazine);
    public Task<Magazine?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<List<Magazine>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<Magazine> GetMagazineAsync(Guid magazineIssueId, CancellationToken cancellationToken = default);
    public Task<List<Magazine>> GetAllAsync(Guid publisherId, CancellationToken cancellationToken=default);
}