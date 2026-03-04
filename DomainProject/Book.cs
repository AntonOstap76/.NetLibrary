namespace DomainProject;

public class Book : CommonEntity
{
    public required string Isbn { get; set; }

    public Book(string title, string content, string isbn)
    {
        Title = title;
        Content = content;
        Isbn = isbn;
    }

    public override string ToString()
    {
        return $"Book called {Title} with code {Isbn} and with content {Content}";
    }
}