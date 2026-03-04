namespace DomainProject;

public class Magazine
{
    public Guid Id { get; private set; }  = Guid.NewGuid();
    public required string Issn { get; set; }
    public required string Title { get; set; }
    public Publisher PublisherId { get; set; }
    public DateTime PublisherDate { get; set; }
    public DateTime EndOfPublish { get; set; }
    public List<MagazineIssue> MagazineIssues { get; set; }

    public Magazine(string issn, string title, Publisher publisherId, DateTime publisherDate, DateTime endOfPublish, List<MagazineIssue> magazineIssues)
    {
        Issn = issn;
        Title = title;
        PublisherId = publisherId;
        PublisherDate = publisherDate;
        EndOfPublish = endOfPublish;
        MagazineIssues = magazineIssues;
    }

    public override string ToString()
    {
        return $"Magazine called {Title} with code {Issn} that start published {PublisherDate} and finished {EndOfPublish}" +
               $"and having this much magazines {MagazineIssues?.Count} and who's publisher is {PublisherId.Id}";
    }
}