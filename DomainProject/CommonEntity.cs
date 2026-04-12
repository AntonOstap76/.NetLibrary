using System.Diagnostics.CodeAnalysis;

namespace DomainProject;

public abstract class CommonEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Content  { get; set; }
    
    protected CommonEntity(string title, string content)
    {
        Title = title;
        Content = content;
    }
}