using DOMAIN.Utilities;

namespace APPLICATION.Contracts;

public interface IRoleService
{
    Task<Response> RequestRole(int userId, string role, int? conferenceId = null);
    Task<object> GetHelperRoleRequests();
    Task<Response> AcceptHelperRoleRequest(int userId);
    Task<Response> RefuseHelperRoleRequest(int userId);
    Task<object> GetManagerRoleRequests();
    Task<Response> AcceptManagerRoleRequest(int userId, int conferenceId);
    Task<Response> RefuseManagerRoleRequest(int userId, int conferenceId);
    Task<object> GetSpeakerRoleRequests(int conferenceId);
    Task<Response> AcceptSpeakerRoleRequest(int userId, int conferenceId);
    Task<Response> RefuseSpeakerRoleRequest(int userId, int conferenceId);
}
