using CommonServiceProject;
using DomainProject;

namespace PublisherServiceProject;

public interface IPublisherService : ICommonCreatorService<Publisher>
{
    public Task<List<Magazine?>> GetAllMagazinesAsync(Guid publisherId, CancellationToken cancellationToken=default);
    public Task<List<Book?>> GetAllBooksAsync(Guid publisherId, CancellationToken cancellationToken=default);
}