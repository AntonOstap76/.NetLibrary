using DomainProject;
using MagazineRepositoryProject;

namespace MagazineServiceProject;

public class MagazineService: IMagazineService
{
    private readonly IMagazineRepository _magazineRepository;

    public MagazineService(IMagazineRepository magazineRepository)
    {
        _magazineRepository = magazineRepository;
    }
    public void Create(Magazine magazine)
    {
        _magazineRepository.Create(magazine);
    }
    public void Update(Magazine magazine)
    {
        _magazineRepository.Update(magazine);
    }

    public void Delete(Magazine magazine)
    {
        _magazineRepository.Delete(magazine);
    }

    public bool Exists(Magazine magazine)
    {
        return _magazineRepository.Exists(magazine);
    }

    public async Task<Magazine?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _magazineRepository.GetByIdAsync(id, cancellationToken);
    }

    public Task<List<Magazine>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _magazineRepository.GetAllAsync(cancellationToken);
    }

    public Task<Magazine> GetMagazineAsync(Guid magazineIssueId, CancellationToken cancellationToken = default)
    {
        return _magazineRepository.GetMagazineAsync(magazineIssueId, cancellationToken);
    }

    public Task<List<Magazine>> GetAllAsync(Guid publisherId, CancellationToken cancellationToken = default)
    {
       return _magazineRepository.GetAllAsync(publisherId, cancellationToken);
    }
}