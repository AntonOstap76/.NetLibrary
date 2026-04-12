using System.Diagnostics.CodeAnalysis;

namespace DomainProject;

public class Publisher : CommonCreator
{
    public DateTime FoundedYear { get; set; }
    public List<Magazine>? Magazines { get; set; }
    
    public Publisher(string name, Country country, DateTime foundedYear, List<CommonEntity>? entities,
        List<Magazine>? magazines) : base(name, country, entities)
    {
        FoundedYear = foundedYear;
        Magazines = magazines;
    }

    public override string ToString()
    {
        return $"Publisher {Name} from {Country.Name} FoundedYear {FoundedYear}" +
               $" with {Entities?.Count} works published";
    }
}