using CommonRepositoryProject;
using DomainProject;

namespace BookRepositoryProject;

public interface IBookRepository : ICommonEntityRepository<Book>
{
    public Task<List<Book>?> GetAllByAuthorAsync(Guid authorId,CancellationToken cancellationToken = default);
    public Task<List<Book>?> GetAllByPublisherAsync(Guid id, CancellationToken cancellationToken = default);
}