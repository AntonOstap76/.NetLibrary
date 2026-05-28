using CommonRepositoryProject;
using CommonServiceProject;
using DomainProject;
using MagazineIssueRepositoryProject;

namespace MagazineIssueServiceProject;

public class MagazineIssueService:CommonEntityService<MagazineIssue>, IMagazineIssueService
{
    private readonly IMagazineIssueRepository _repository;
    public MagazineIssueService(IMagazineIssueRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public Task<List<MagazineIssue?>> GetAllByMagazineIdAsync(Guid magazineId, CancellationToken cancellationToken = default)
    {
        return _repository.GetAllByMagazineIdAsync(magazineId, cancellationToken);
    }
}