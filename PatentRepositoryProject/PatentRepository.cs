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
        var patents = _database.FindAll(p=>p.Authors.Any(a=>a.Id == author.Id));
        if (!patents.Any())
        {
            return Task.FromResult<List<Patent?>>(null);
        }
        return Task.FromResult<List<Patent?>>(patents);
    }
    
    public Task<List<Patent?>> GetAsync(IEnumerable<Author> authors, CancellationToken cancellationToken = default)
    {
        if (authors == null || !authors.Any())
            return Task.FromResult(new List<Patent?>());

        var patents = _database.FindAll(p => p.Authors.Any(a => authors.Any(i => i.Id == a.Id)));

        if (!patents.Any())
            return Task.FromResult(new List<Patent?>());

        return Task.FromResult<List<Patent?>>(patents);    
    }
}