using System.Diagnostics.CodeAnalysis;
using System.Security.AccessControl;

namespace DomainProject;

public class Author : CommonCreator
{
    public string LastName { get; set; }
    public DateTime BirthdayYear { get; set; }
    public Patent PatentId { get; set; }
    
    public Author(string name, string lastName, Country country, DateTime birthdayYear, List<CommonEntity>? entities,
        Patent patentId) : base(name, country, entities)
    {
        LastName = lastName;
        BirthdayYear = birthdayYear;
        PatentId = patentId;
    }

    public override string ToString()
    {
        return
            $"Author {Name} {LastName} from {Country.Name} born in {BirthdayYear} having this much works {Entities?.Count}";
    }
}