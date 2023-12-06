using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PERSISTANCE;

public interface IConnectionFactory
{
    public IDbConnection CreateMSSQLConnection();
}
public class ConnectionFactory : IConnectionFactory
{
    private readonly IConfiguration _config;
    public ConnectionFactory(IConfiguration config) => _config = config;
    public IDbConnection CreateMSSQLConnection()
    {
        //TODO: Crypt and decrypt connection string
        IDbConnection connection = new SqlConnection(_config.GetSection("DBConnections:SqlServer").Value);

        connection.Open();
        return connection;
    }
}
