using DOMAIN.Utilities;
using System.Data;
using System.Data.SqlClient;

namespace PERSISTANCE;

public interface IConnectionFactory
{
    public IDbConnection CreateMSSQLConnection();
}
public class ConnectionFactory : IConnectionFactory
{
    private readonly AppSettings _appSettings;
    public ConnectionFactory(AppSettings appSettings) => _appSettings = appSettings;
    public IDbConnection CreateMSSQLConnection()
    {
        //TODO: Crypt and decrypt connection string
        IDbConnection connection = new SqlConnection(_appSettings.DBConnections.SqlServer);

        connection.Open();
        return connection;
    }
}
