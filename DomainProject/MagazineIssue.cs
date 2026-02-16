namespace DomainProject;

public class MagazineIssue : CommonEntity
{
    public int Number { get; set; }
    public DateTime PublisherDate { get; set; }
    public required string MagazineIssn { get; set; }
    public Magazine Magazine { get; set; }

    public MagazineIssue(int number, string title, string content, DateTime publisherDate, string magazineIssn)
    {
        Number = number;
        Title = title;
        Content = content;
        PublisherDate = publisherDate;
        MagazineIssn = magazineIssn;
    }
}