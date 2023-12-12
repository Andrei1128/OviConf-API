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
}