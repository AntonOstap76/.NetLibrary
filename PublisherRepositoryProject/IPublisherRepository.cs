using CommonRepositoryProject;
using DomainProject;

namespace PublisherRepositoryProject;

public interface IPublisherRepository : ICommonCreatorRepository<Publisher>
{ 
    public Task<List<Magazine>?> GetAllMagazinesAsync(Guid publisherId, CancellationToken cancellationToken = default);
    public Task<List<Book?>> GetAllBooksAsync(Guid publisherId, CancellationToken cancellationToken=default);
}