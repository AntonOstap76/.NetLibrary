using BookRepositoryProject;
using CommonRepositoryProject;
using CommonServiceProject;
using DomainProject;

namespace BookServiceProject;
using ValidatorProject;
using ValidatorProject.Numbers;
public class BookService : CommonEntityService<Book>,IBookService
{
    private readonly IBookRepository _repository;
    public BookService(IBookRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public Task<List<Book>?> GetAllByAuthorAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        return _repository.GetAllByAuthorAsync(authorId, cancellationToken);
    }

    public Task<List<Book?>> GetAllByPublisherAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _repository.GetAllByPublisherAsync(id, cancellationToken);
    }
}