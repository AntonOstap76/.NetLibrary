using CommonRepositoryProject;
using DomainProject;

namespace AuthorRepositoryProject;

public class AuthorRepository : CommonCreatorRepository<Author>, IAuthorRepository
{
    public Task<List<Patent?>> GetAllPatentsAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Book?>> GetAllBooksAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Author?>> GetAuthorAsync(Guid patentId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}