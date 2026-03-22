namespace DomainProject;

public class Book : CommonEntity
{
    public required string Isbn { get; init; }
    public List<Author> AuthorIDs { get; set; }
    public Publisher PublisherID { get; set; }

    public Book(string title, string content, string isbn, List<Author> authorId, Publisher publisherId)
    {
        Title = title;
        Content = content;
        Isbn = isbn;
        AuthorIDs = authorId;
        PublisherID = publisherId;
    }

    public override string ToString()
    {
        return $"Book called {Title} with code {Isbn} and with content {Content}";
    }
}