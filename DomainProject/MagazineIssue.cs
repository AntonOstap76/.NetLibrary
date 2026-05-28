using System.Diagnostics.CodeAnalysis;

namespace DomainProject;

public class MagazineIssue : CommonEntity
{
    public int Number { get; set; }
    public DateTime PublisherDate { get; set; }
    public string MagazineIssn { get; set; }
    public Magazine Magazine { get; set; }
    
    public MagazineIssue(int number, string title, string content, DateTime publisherDate, string magazineIssn, Magazine magazine) :
        base(title, content)
    {
        Number = number;
        PublisherDate = publisherDate;
        MagazineIssn = magazineIssn;
        Magazine = magazine;
    }

    public override string ToString()
    {
        return $"Magazine called {Title} with code {MagazineIssn} which is a {Number} from {Magazine.Title}" +
               $"that started publishing {PublisherDate} with content {Content}";
    }
}