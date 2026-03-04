using CommonRepositoryProject;

namespace BookRepositoryProject;

public class BookDTO :CommonEntityDTO
{
    public required string Isbn { get; set; }
}