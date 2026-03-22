using System.Reflection.Metadata.Ecma335;
using AuthorRepositoryProject;
using CommonRepositoryProject;
using CommonServiceProject;
using DomainProject;

namespace AuthorServiceProject;

public class AuthorService:CommonCreatorService<Author>,IAuthorService
{
    private readonly IAuthorRepository _repository;
    public AuthorService(IAuthorRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public Task<List<Patent?>> GetAllPatentsAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        return _repository.GetAllPatentsAsync(authorId, cancellationToken);
    }

    public Task<List<Book?>> GetAllBooksAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        return  _repository.GetAllBooksAsync(authorId, cancellationToken);
    }

    public Task<Author?> GetAuthorAsync(Guid patentId, CancellationToken cancellationToken = default)
    {
        return  _repository.GetAuthorAsync(patentId, cancellationToken);
    }
}