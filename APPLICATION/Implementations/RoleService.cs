using APPLICATION.Contracts;
using DOMAIN.Models;
using DOMAIN.Utilities;
using PERSISTANCE.Contracts;
using System.Data.SqlClient;

namespace APPLICATION.Implementations;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly ThisUser _thisUser;
    public RoleService(IRoleRepository roleRepository, ThisUser thisUser)
    {
        _thisUser = thisUser;
        _roleRepository = roleRepository;
    }

    public async Task<IEnumerable<RoleRequest>> GetRoleRequests(string role, int? conferenceId = null) => await _roleRepository.GetRoleRequests(role, conferenceId);

    public async Task<Response> AcceptRoleRequest(int requestId)
    {
        var response = new Response();

        await _roleRepository.AcceptRoleRequest(requestId, _thisUser.Id);

        response.IsSuccess = true;
        response.Message = "Request accepted succesfully!";
        return response;
    }

    public async Task<Response> RefuseRoleRequest(int requestId)
    {
        var response = new Response();

        await _roleRepository.RefuseRoleRequest(requestId, _thisUser.Id);

        response.IsSuccess = true;
        response.Message = "Request refused succesfully!";
        return response;
    }

    public async Task<Response> RequestRole(string role, int? conferenceId = null)
    {
        var response = new Response();

        try
        {
            await _roleRepository.RequestRole(_thisUser.Id, role, conferenceId);

            response.IsSuccess = true;
            response.Message = "Request done succesfully!";
            return response;
        }
        catch (SqlException ex)
        {
            if (ex.Number == SqlExceptionCodes.UNIQUE_CONSTRAINT_VIOLATION)
            {
                response.Message = "You already have this role or role request!";
                return response;
            }
            else throw;
        }
    }
}
