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
}