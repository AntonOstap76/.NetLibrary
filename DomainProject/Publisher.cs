namespace DomainProject;

public class Publisher: CommonCreator
{
    public DateTime FoundedYear { get; set; }
    public List<Magazine>? Magazines { get; set; }

    public Publisher(string name, Country country, DateTime foundedYear, List<CommonEntity>? entities, List<Magazine>? magazines)
    {
        Name = name;
        Country = country;
        FoundedYear = foundedYear;
        Entities = entities;
        Magazines = magazines;
    }

    public override string ToString()
    {
        return $"Publisher {Name} from {Country.Name} FoundedYear {FoundedYear}" +
               $" with {Entities?.Count} works published";
    }
}