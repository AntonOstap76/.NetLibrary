namespace DomainProject;

public class LibraryException: Exception
{
    public LibraryException():base("There was an error processing your request")
    {
        
    }
    public LibraryException(string message)
        : base(message)
    {
    }

    public LibraryException(string message, Exception inner)
        : base(message, inner)
    {
    }
}