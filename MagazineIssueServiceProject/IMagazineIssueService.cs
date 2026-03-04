using CommonServiceProject;
using DomainProject;

namespace MagazineIssueServiceProject;

public interface IMagazineIssueService : ICommonEntityService<MagazineIssue>
{
    public Task<List<MagazineIssue?>> GetAllByMagazineIdAsync(Guid magazineId, CancellationToken cancellationToken = default);
}