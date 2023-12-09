using Dapper;
using DOMAIN.Models;
using PERSISTANCE.Contracts;
using PERSISTANCE.Queries;
using System.Data;

namespace PERSISTANCE.Implementations;

public class AuthRepository : IAuthRepository
{
    private readonly IConnectionFactory _connectionFactory;
    public AuthRepository(IConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;
    public async Task<User?> GetUserData(string email)
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("p_email", email, DbType.String);

        return await connection.ExecuteScalarAsync<User>(AuthQueries.GET_USER_DATA, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Role>> GetUserRoles(int userId)
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("p_userId", userId, DbType.String);

        return await connection.QueryAsync<Role>(AuthQueries.GET_USER_ROLES, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task RegisterUser(string email, string password)
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("p_email", email, DbType.String);
        parameters.Add("p_password", password, DbType.String);

        await connection.ExecuteAsync(AuthQueries.REGISTER_USER, parameters, commandType: CommandType.StoredProcedure);
    }
}
