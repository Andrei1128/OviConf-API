using APPLICATION.Contracts;
using DOMAIN.Models;
using DOMAIN.Utilities;
using PERSISTANCE.Contracts;
using System.Data.SqlClient;

namespace APPLICATION.Implementations;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    public RoleService(IRoleRepository roleRepository) => _roleRepository = roleRepository;

    public async Task<Response> AcceptRoleRequest(int userId, string role, int? conferenceId = null)
    {
        var response = new Response();

        await _roleRepository.AcceptRoleRequest(userId, role, conferenceId);

        response.IsSucces = true;
        response.Message = "Request accepted succesfully!";
        return response;
    }

    public async Task<IEnumerable<Role>> GetRoleRequests(string role, int? conferenceId = null) => await _roleRepository.GetRoleRequests(role, conferenceId);

    public async Task<Response> RefuseRoleRequest(int userId, string role, int? conferenceId = null)
    {
        var response = new Response();

        await _roleRepository.RefuseRoleRequest(userId, role, conferenceId);

        response.IsSucces = true;
        response.Message = "Request refused succesfully!";
        return response;
    }

    public async Task<Response> RequestRole(int userId, string role, int? conferenceId = null)
    {
        var response = new Response();

        try
        {
            await _roleRepository.RequestRole(userId, role, conferenceId);

            response.IsSucces = true;
            response.Message = "Request done succesfully!";
            return response;
        }
        catch (SqlException ex)
        {
            if (ex.Number == SqlExceptionCodes.UNIQUE_CONSTRAINT_VIOLATION)
            {
                response.Message = "You already have this role or a higher one!";
                return response;
            }
            else throw;
        }
    }
}
