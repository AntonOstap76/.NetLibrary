using CommonRepositoryProject;
using DomainProject;

namespace BookRepositoryProject;

public class BookRepository : CommonEntityRepository<Book>, IBookRepository
{
    public Task<List<Book>?> GetAllByAuthorAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        var books = _database.FindAll(b => b.AuthorIDs.Any(a => a.Id == authorId));
        if (!books.Any())
        {
            return Task.FromResult<List<Book>?>(null); 
        }
        return Task.FromResult<List<Book>?>(books);
    }

    public Task<List<Book>?> GetAllByPublisherAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var books = _database.FindAll(b => b.PublisherID.Id == id);
        if (!books.Any())
        {
            return Task.FromResult<List<Book>?>(null); 
        }
        return Task.FromResult<List<Book>?>(books);
    }
}