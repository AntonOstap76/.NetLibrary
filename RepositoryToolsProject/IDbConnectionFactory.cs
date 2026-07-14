using System.Data;

namespace RepositoryToolsProject;

public interface IDbConnectionFactory
{
     IDbConnection CreateConnection();
}