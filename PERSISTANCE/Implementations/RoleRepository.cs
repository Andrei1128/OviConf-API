﻿using Dapper;
using DOMAIN.Models;
using PERSISTANCE.Contracts;
using PERSISTANCE.Queries;
using System.Data;

namespace PERSISTANCE.Implementations;

public class RoleRepository : IRoleRepository
{
    private readonly IConnectionFactory _connectionFactory;
    public RoleRepository(IConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

    public async Task AcceptRoleRequest(int requestId, int operatedByUserId)
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("p_requestId", requestId, DbType.Int32);
        parameters.Add("p_operatedBy", operatedByUserId, DbType.Int32);

        await connection.ExecuteAsync(RoleQueries.ACCEPT_ROLE_REQUEST, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RoleRequest>> GetAllRoleRequests()
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        return await connection.QueryAsync<RoleRequest>(RoleQueries.GET_ALL_ROLE_REQUESTS, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RoleRequest>> GetRoleRequests(string role, int? conferenceId = null)
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("p_role", role, DbType.String);
        parameters.Add("p_conferenceId", conferenceId, DbType.Int32);

        return await connection.QueryAsync<RoleRequest>(RoleQueries.GET_ROLE_REQUESTS, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task RefuseRoleRequest(int requestId)
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("p_requestId", requestId, DbType.Int32);

        await connection.ExecuteAsync(RoleQueries.REFUSE_ROLE_REQUESTS, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task RequestRole(int userId, string role, int? conferenceId = null)
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("p_userId", userId, DbType.Int32);
        parameters.Add("p_role", role, DbType.String);
        parameters.Add("p_conferenceId", conferenceId, DbType.Int32);

        await connection.ExecuteAsync(RoleQueries.REQUEST_ROLE, parameters, commandType: CommandType.StoredProcedure);
    }
}
