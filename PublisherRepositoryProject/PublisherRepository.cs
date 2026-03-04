using CommonRepositoryProject;
using DomainProject;

namespace PublisherRepositoryProject;

public class PublisherRepository : CommonCreatorRepository<Publisher>, IPublisherRepository
{
    public Task<List<Magazine?>> GetAllMagazinesAsync(Guid publisherId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Book?>> GetAllBooksAsync(Guid publisherId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}