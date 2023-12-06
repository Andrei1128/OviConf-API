using APPLICATION.Contracts;
using DOMAIN.Utilities;
using PERSISTANCE.Contracts;

namespace APPLICATION.Implementations;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    public RoleService(IRoleRepository roleRepository) => _roleRepository = roleRepository;

    public async Task<Response> AcceptHelperRoleRequest(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> AcceptManagerRoleRequest(int userId, int conferenceId)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> AcceptSpeakerRoleRequest(int userId, int conferenceId)
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

    public async Task<object> GetSpeakerRoleRequests(int conferenceId)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> RefuseHelperRoleRequest(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> RefuseManagerRoleRequest(int userId, int conferenceId)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> RefuseSpeakerRoleRequest(int userId, int conferenceId)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> RequestRole(int userId, string role, int? conferenceId = null)
    {
        var response = new Response();

        if (await _roleRepository.RequestRole(userId, role, conferenceId))
        {
            response.IsSucces = true;
            response.Message = "Request done succesfully!";
        }

        response.Message = "You already have this role or a higher one!";
        return response;
    }
}
