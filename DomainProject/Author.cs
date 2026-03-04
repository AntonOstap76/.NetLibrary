namespace DomainProject;

public class Author : CommonCreator
{
    public required string LastName { get; set; }
    public DateTime BirthdayYear { get; set; }
    

    public Author(string name,string lastName, Country country,  DateTime birthdayYear, List<CommonEntity>? entities)
    {
        Name = name;
        LastName = lastName;
        Country = country;
        BirthdayYear = birthdayYear;
        Entities = entities;
    }

    public override string ToString()
    {
        return $"Author {Name} {LastName} from {Country.Name} born in {BirthdayYear} having this much works {Entities?.Count}";
    }
}