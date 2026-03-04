using CommonRepositoryProject;
using DomainProject;

namespace MagazineIssueRepositoryProject;

public interface IMagazineIssueRepository : ICommonEntityRepository<MagazineIssue>
{
    public Task<List<MagazineIssue?>> GetAllByMagazineIdAsync(Guid magazineId, CancellationToken cancellationToken = default);
}