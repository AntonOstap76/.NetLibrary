using System.ComponentModel.DataAnnotations;
using DomainProject;

namespace CommonRepositoryProject;

public class CommonCreatorDTO
{
    [Required]
    public required string Name { get; set; }
    //public required CountryDTO Country  { get; set; }
    public List<CommonEntity>? Entities { get; set; }
    
    
}