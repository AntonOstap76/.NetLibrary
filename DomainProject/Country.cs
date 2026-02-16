namespace DomainProject;

public class Country
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required string Language { get; set; }

    public Country(string name, string code, string language)
    {
        Name = name;
        Code = code;
        Language = language;
    }
}