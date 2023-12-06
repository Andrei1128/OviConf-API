using Dapper;
using PERSISTANCE.Contracts;
using PERSISTANCE.Queries;
using System.Data;

namespace PERSISTANCE.Implementations;

public class RoleRepository : IRoleRepository
{
    private readonly IConnectionFactory _connectionFactory;
    public RoleRepository(IConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

    public async Task<bool> AcceptHelperRoleRequest(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AcceptManagerRoleRequest(int userId, int conferenceId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AcceptSpeakerRoleRequest(int userId, int conferenceId)
    {
        throw new NotImplementedException();
    }

    public async Task<object> GetHelperRoleRequests()
    {
        throw new NotImplementedException();
    }

    public async Task<object> GetManagerRoleRequests()
    {
        throw new NotImplementedException();
    }

    public async Task<object> GetSpeakerRoleRequests()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RefuseHelperRoleRequest(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RefuseManagerRoleRequest(int userId, int conferenceId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RefuseSpeakerRoleRequest(int userId, int conferenceId)
    {
        throw new NotImplementedException();
    }

    public async Task RequestRole(int userId, string role, int? conferenceId = null)
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("p_userId", userId, DbType.Int32);
        parameters.Add("p_requestedRole", role, DbType.String);
        parameters.Add("p_conferenceId", conferenceId, DbType.Int32);

        await connection.ExecuteAsync(RoleQueries.REQUEST_ROLE, parameters, commandType: CommandType.StoredProcedure);
    }
}
