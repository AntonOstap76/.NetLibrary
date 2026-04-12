using System.Diagnostics.CodeAnalysis;

namespace DomainProject;

public class Country
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Language { get; set; }
    
    public Country(string name, string code, string language)
    {
        Name = name;
        Code = code;
        Language = language;
    }

    public override string ToString()
    {
        return $"Country called {Name} with code {Code} with language {Language}";
    }
}