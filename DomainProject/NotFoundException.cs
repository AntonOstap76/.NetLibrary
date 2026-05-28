namespace DomainProject;

public class NotFoundException : LibraryException
{
    public NotFoundException(string entityName, Guid id)
        : base($"{entityName} with id {id} was not found")
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}