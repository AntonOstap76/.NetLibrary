using CommonRepositoryProject;
using DomainProject;

namespace PatentRepositoryProject;

public class PatentRepository : CommonEntityRepository<Patent>, IPatentRepository
{
    public Task<List<Patent?>> GetAsync(Author author, CancellationToken cancellationToken = default)
    {
        if (author == null)
            throw new ArgumentNullException(nameof(author));
        
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
            throw new ArgumentNullException(nameof(authors));

        var patents = _database.FindAll(p => p.Authors.Any(a => authors.Any(i => i.Id == a.Id)));

        if (!patents.Any())
            return Task.FromResult(new List<Patent?>());

        return Task.FromResult<List<Patent?>>(patents);    
    }
}