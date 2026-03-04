using CommonRepositoryProject;
using CommonServiceProject;
using DomainProject;
using PublisherRepositoryProject;

namespace PublisherServiceProject;

public class PublisherService : CommonCreatorService<Publisher>,  IPublisherService
{
    private readonly IPublisherRepository _repository;
    public PublisherService(IPublisherRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public Task<List<Magazine?>> GetAllMagazinesAsync(Guid publisherId, CancellationToken cancellationToken = default)
    {
        return  _repository.GetAllMagazinesAsync(publisherId, cancellationToken);
    }

    public Task<List<Book?>> GetAllBooksAsync(Guid publisherId, CancellationToken cancellationToken = default)
    {
        return  _repository.GetAllBooksAsync(publisherId, cancellationToken);
    }
}