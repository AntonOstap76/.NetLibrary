using System.ComponentModel.DataAnnotations;

namespace CommonRepositoryProject;

public class CommonEntityDTO
{
    [Required]
    public required string Title { get; set; }
    [Required]
    public required string Content { get; set; }
}