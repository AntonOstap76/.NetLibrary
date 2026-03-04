using CommonRepositoryProject;
using DomainProject;

namespace BookRepositoryProject;

public class BookRepository : CommonEntityRepository<Book>, IBookRepository
{
    public Task<List<Book>?> GetAllByAuthorAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Book?>> GetAllByPublisherAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}