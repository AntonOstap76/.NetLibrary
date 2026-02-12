namespace DomainProject;

public abstract class CommonEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required string Title { get; set; }
    public required string Content  { get; set; }
    
}