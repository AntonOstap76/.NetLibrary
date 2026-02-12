namespace UserDomainProject;

public class User
{
    public Guid Id { get; private set; } =  Guid.NewGuid();
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public required string ImageUrl { get; set; }

    public User(string username, string password, string email, string imageUrl)
    {
        Username = username;
        Password = password;
        Email = email;
        ImageUrl = imageUrl;
    }
}