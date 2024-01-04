using Dapper;
using DOMAIN.DTOs;
using DOMAIN.Models;
using PERSISTANCE.Contracts;
using PERSISTANCE.Queries;
using System.Data;

namespace PERSISTANCE.Implementations;

public class ConferenceRepository : IConferenceRepository
{
    private readonly IConnectionFactory _connectionFactory;
    public ConferenceRepository(IConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;
    public async Task CreateConference(ConferenceDTO payload)
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("p_name", payload.Name, DbType.String);
        parameters.Add("p_startDate", payload.StartDate, DbType.DateTime);
        parameters.Add("p_endDate", payload.EndDate, DbType.DateTime);

        await connection.ExecuteAsync(ConferenceQueries.CREATE_CONFERENCE, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<Conference?> GetConference(int id)
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("p_id", id, DbType.Int32);

        return await connection.QuerySingleOrDefaultAsync<Conference>(ConferenceQueries.GET_CONFERENCE, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Conference>> GetConferences()
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        return await connection.QueryAsync<Conference>(ConferenceQueries.GET_CONFERENCES, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Conference>> GetMyConferences(int userId)
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("p_userId", userId, DbType.Int32);

        return await connection.QueryAsync<Conference>(ConferenceQueries.GET_MY_CONFERENCES, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<UserDTO>> GetPeople(int conferenceId, string role)
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("p_conferenceId", conferenceId, DbType.Int32);
        parameters.Add("p_role", role, DbType.String);

        return await connection.QueryAsync<UserDTO>(ConferenceQueries.GET_PEOPLES, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task RegisterAtConference(int conferenceId, int userId)
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("p_conferenceId", conferenceId, DbType.Int32);
        parameters.Add("p_userId", userId, DbType.Int32);

        await connection.ExecuteAsync(ConferenceQueries.REGISTER_AT_CONFERENCE, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task AddNavItem(NavItem navItem)
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("p_conferenceId", navItem.ConferenceId, DbType.Int32);
        parameters.Add("p_title", navItem.Title, DbType.String);
        parameters.Add("p_content", navItem.Content, DbType.String);
        parameters.Add("p_order", navItem.Order ?? 0, DbType.Int32);

        await connection.ExecuteAsync(ConferenceQueries.ADD_NAV_ITEM, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateNavItem(NavItem navItem)
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("p_id", navItem.Id, DbType.Int32);
        parameters.Add("p_title", navItem.Title, DbType.String);
        parameters.Add("p_content", navItem.Content, DbType.String);
        parameters.Add("p_order", navItem.Order ?? 0, DbType.Int32);

        await connection.ExecuteAsync(ConferenceQueries.UPDATE_NAV_ITEM, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateConference(ConferenceDTO payload)
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("p_id", payload.Id, DbType.Int32);
        parameters.Add("p_name", payload.Name, DbType.String);
        parameters.Add("p_startDate", payload.StartDate, DbType.DateTime);
        parameters.Add("p_endDate", payload.EndDate, DbType.DateTime);

        await connection.ExecuteAsync(ConferenceQueries.UPDATE_CONFERENCE, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<NavItemDTO>> GetNavItems(int conferenceId)
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("p_conferenceId", conferenceId, DbType.Int32);

        return await connection.QueryAsync<NavItemDTO>(ConferenceQueries.GET_NAV_ITEMS, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<string> GetNavItemContent(int navItemId)
    {
        using var connection = _connectionFactory.CreateMSSQLConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("p_id", navItemId, DbType.Int32);

        return await connection.QuerySingleOrDefaultAsync<string>(ConferenceQueries.GET_NAV_ITEM_CONTENT, parameters, commandType: CommandType.StoredProcedure) ?? string.Empty;
    }
}