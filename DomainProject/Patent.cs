using System.Diagnostics.CodeAnalysis;

namespace DomainProject;

public class Patent:CommonEntity
{
    public string PatentCode { get; set; }
    public DateTime PublishDate { get; set; }
    
    //Authors
    public List<Author> Authors { get; set; }
    
    public Patent(string title, string content, string patentCode, DateTime publishDate,  List<Author> authors) : base(title, content)
    {
        PatentCode = patentCode;
        PublishDate = publishDate;
        Authors = authors;
    }

    public override string ToString()
    {
        return $"Patent {Title} with code {PatentCode} that was published {PublishDate}" +
               $" and have {Authors.Count} authors with content:{Content}";
    }
}