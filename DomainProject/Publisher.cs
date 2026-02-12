namespace DomainProject;

public class Publisher: CommonCreator
{
    public DateTime FoundedYear { get; set; }

    public Publisher(string name, Country country, DateTime foundedYear, List<CommonEntity>? entities)
    {
        Name = name;
        Country = country;
        FoundedYear = foundedYear;
        Entities = entities;
    }
}