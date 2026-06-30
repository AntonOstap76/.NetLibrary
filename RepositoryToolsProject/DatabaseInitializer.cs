using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using RepositoryToolsProject;

public class DatabaseInitializer
{
    private readonly IDbConnectionFactory _factory;

    public DatabaseInitializer(IDbConnectionFactory factory, IConfiguration configuration)
    {
        _factory = factory;
    }
    
}