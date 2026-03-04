using CommonRepositoryProject;
using DomainProject;

namespace AuthorRepositoryProject;

public interface IAuthorRepository : ICommonCreatorRepository<Author>
{   /// <summary>
    /// Get entities for the current creator entity
    /// </summary>
    /// <param name="authorId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Patent?>> GetAllPatentsAsync(Guid authorId, CancellationToken cancellationToken=default);
    public Task<List<Book?>> GetAllBooksAsync(Guid authorId, CancellationToken cancellationToken=default);
    public Task<List<Author?>> GetAuthorAsync(Guid patentId, CancellationToken cancellationToken = default);

}