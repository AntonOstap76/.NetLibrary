using CommonServiceProject;
using DomainProject;

namespace PatentServiceProject;

public interface IPatentService:ICommonEntityService<Patent>
{
    public Task<List<Patent?>> GetAsync(Author author, CancellationToken cancellationToken=default); 
    public Task<List<Patent?>> GetAsync(Publisher publisher, CancellationToken cancellationToken=default);
    public Task<List<Patent?>> GetAsync(IEnumerable<Author> authors, CancellationToken cancellationToken=default); 
}