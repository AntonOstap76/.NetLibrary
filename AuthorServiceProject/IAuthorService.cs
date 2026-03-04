using CommonServiceProject;
using DomainProject;

namespace AuthorServiceProject;

public interface IAuthorService : ICommonCreatorService<Author>
{
    public Task<List<Patent?>> GetAllPatentsAsync(Guid authorId, CancellationToken cancellationToken=default);
    public Task<List<Book?>> GetAllBooksAsync(Guid authorId, CancellationToken cancellationToken=default);
    public Task<List<Author?>> GetAuthorAsync(Guid patentId, CancellationToken cancellationToken = default);
}