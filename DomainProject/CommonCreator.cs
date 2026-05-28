using System.Diagnostics.CodeAnalysis;

namespace DomainProject;

public abstract class CommonCreator
{
    public Guid Id { get; private set; } =  Guid.NewGuid();
    public string Name { get; set; }
    public Country Country { get; set; }
    public List<CommonEntity>? Entities { get; set; }
    
    protected CommonCreator(string name, Country country, List<CommonEntity>? entities)
    {
        Name = name;
        Country = country;
        Entities = entities;
    }
}