namespace DomainProject;

public abstract class CommonCreator
{
    public Guid Id { get; private set; } =  Guid.NewGuid();
    public required string Name { get; set; }
    public required Country Country { get; set; }
    public List<CommonEntity>? Entities { get; set; }
}