using System.ComponentModel.DataAnnotations;

namespace CommonRepositoryProject;

public class CountryDTO
{
    [Required]
    public required string Name { get; set; }
}