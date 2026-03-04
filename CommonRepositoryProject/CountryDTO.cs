using System.ComponentModel.DataAnnotations;

namespace CommonRepositoryProject;

public class CountryDTO
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required string Language { get; set; }
}