namespace DomainProject;

public class Magazine
{
    public Guid Id { get; private set; }  = Guid.NewGuid();
    public required string Issn { get; set; }
    public required string Title { get; set; }
    //Publisher ID
    public Guid PublisherId { get; set; }
    public DateTime PublisherDate { get; set; }
    public DateTime EndOfPublish { get; set; }
    //MagazineIssue
    public List<MagazineIssue> MagazineIssues { get; set; }
}