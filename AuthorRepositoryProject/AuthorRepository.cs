using CommonRepositoryProject;
using DomainProject;

namespace AuthorRepositoryProject;

public class AuthorRepository : CommonCreatorRepository<Author>, IAuthorRepository
{
    public Task<List<Patent?>> GetAllPatentsAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        var author = _databaseCreator.FirstOrDefault(a => a.Id == authorId);

        if (author == null)
            return Task.FromResult(new List<Patent?>());

        var patents = author.Entities?
            .OfType<Patent>()
            .Cast<Patent?>()
            .ToList();

        if (patents == null || !patents.Any())
            return Task.FromResult(new List<Patent?>());

        return Task.FromResult(patents);
    }

    public Task<List<Book?>> GetAllBooksAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        var author = _databaseCreator.FirstOrDefault(a => a.Id == authorId);

        if (author == null)
            return Task.FromResult(new List<Book?>());

        var books = author.Entities?
            .OfType<Book>()
            .Cast<Book?>()
            .ToList();

        if (books == null || !books.Any())
            return Task.FromResult(new List<Book?>());

        return Task.FromResult(books);
    }

    public Task<Author?> GetAuthorAsync(Guid patentId, CancellationToken cancellationToken = default)
    {
        var author = _databaseCreator.FirstOrDefault(p => p.PatentId.Id == patentId);
        if (author == null)
        {
            return Task.FromResult<Author?>(null);
        }
        return Task.FromResult(author);
    }
}