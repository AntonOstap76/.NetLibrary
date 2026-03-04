using CommonRepositoryProject;
using DomainProject;

namespace MagazineIssueRepositoryProject;

public class MagazineIssueDTO : CommonEntityDTO
{
    public int Number { get; set; }
    public DateTime PublisherDate { get; set; }
    public required string MagazineIssn { get; set; }
    public Magazine Magazine { get; set; }
}