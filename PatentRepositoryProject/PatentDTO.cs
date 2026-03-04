using CommonRepositoryProject;
using DomainProject;

namespace PatentRepositoryProject;

public class PatentDTO : CommonEntityDTO
{
    public required string PatentCode { get; set; }
    public DateTime PublishDate { get; set; }
    //Authors
    public List<Author> Authors { get; set; }
}