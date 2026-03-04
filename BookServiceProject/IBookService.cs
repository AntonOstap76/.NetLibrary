using CommonServiceProject;
using DomainProject;

namespace BookServiceProject;

public interface IBookService : ICommonEntityService<Book>
{
    public Task<List<Book>?> GetAllByAuthorAsync(Guid authorId,CancellationToken cancellationToken = default);
    public Task<List<Book?>> GetAllByPublisherAsync(Guid id, CancellationToken cancellationToken = default);
}