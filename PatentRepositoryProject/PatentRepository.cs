using CommonRepositoryProject;
using DomainProject;

namespace PatentRepositoryProject;

public class PatentRepository : CommonEntityRepository<Patent>, IPatentRepository
{
    public Task<List<Author?>> GetAuthorAsync(Guid patentId, CancellationToken cancellationToken=default)
    {
       var entity = _database.FirstOrDefault(x => x.Id == patentId);
       return Task.FromResult(entity.Authors);
    }

    public Task<List<Patent?>> GetAsync(Author author, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Patent?>> GetAsync(Publisher publisher, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Patent?>> GetAsync(IEnumerable<Author> authors, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}