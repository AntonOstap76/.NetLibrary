using CommonRepositoryProject;
using DomainProject;

namespace PublisherRepositoryProject;

public class PublisherRepository : CommonCreatorRepository<Publisher>, IPublisherRepository
{
    public Task<List<Magazine>?> GetAllMagazinesAsync(Guid publisherId, CancellationToken cancellationToken = default)
    {

        var publisher = _databaseCreator.FirstOrDefault(p => p.Id == publisherId);

        if (publisher == null)
            throw new NotFoundException(nameof(Publisher), publisherId);

        var magazines = publisher.Magazines;

        if (magazines == null || !magazines.Any())
            return Task.FromResult(new List<Magazine>());

        return Task.FromResult(magazines);
    }

    public Task<List<Book?>> GetAllBooksAsync(Guid publisherId, CancellationToken cancellationToken = default)
    {
        var publisher = _databaseCreator.FirstOrDefault(p => p.Id == publisherId);

        if (publisher == null)
            throw new NotFoundException(nameof(Publisher), publisherId);

        var books = publisher.Entities?
            .OfType<Book>()
            .Cast<Book?>()
            .ToList();;

        if (books == null || !books.Any())
            return Task.FromResult(new List<Book>());

        return Task.FromResult(books);
    }
}