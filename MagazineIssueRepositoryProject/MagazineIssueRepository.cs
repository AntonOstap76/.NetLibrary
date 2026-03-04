using CommonRepositoryProject;
using DomainProject;

namespace MagazineIssueRepositoryProject;

public class MagazineIssueRepository: CommonEntityRepository<MagazineIssue>,  IMagazineIssueRepository
{
    public Task<List<MagazineIssue?>> GetAllByMagazineIdAsync(Guid magazineId, CancellationToken cancellationToken = default)
    {
        var magIssues = _database.FindAll(i=>i.Magazine.Id == magazineId);
        if (!magIssues.Any())
        {
            return Task.FromResult(new List<MagazineIssue>());
        }
        return Task.FromResult(magIssues);
    }
}