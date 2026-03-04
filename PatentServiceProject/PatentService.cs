using CommonRepositoryProject;
using CommonServiceProject;
using DomainProject;
using PatentRepositoryProject;

namespace PatentServiceProject;

public class PatentService: CommonEntityService<Patent>, IPatentService
{
    private readonly IPatentRepository _repository;
    public PatentService(IPatentRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public Task<List<Patent?>> GetAsync(Author author, CancellationToken cancellationToken = default)
    {
       return _repository.GetAsync(author, cancellationToken);
    }

    public Task<List<Patent?>> GetAsync(Publisher publisher, CancellationToken cancellationToken = default)
    {
        return _repository.GetAsync(publisher, cancellationToken);
    }

    public Task<List<Patent?>> GetAsync(IEnumerable<Author> authors, CancellationToken cancellationToken = default)
    {
        return  _repository.GetAsync(authors, cancellationToken);
    }
}