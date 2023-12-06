namespace PERSISTANCE.Contracts;

public interface IRoleRepository
{
    Task RequestRole(int userId, string role, int? conferenceId = null);
    Task<object> GetHelperRoleRequests();
    Task<bool> AcceptHelperRoleRequest(int userId);
    Task<bool> RefuseHelperRoleRequest(int userId);
    Task<object> GetManagerRoleRequests();
    Task<bool> AcceptManagerRoleRequest(int userId, int conferenceId);
    Task<bool> RefuseManagerRoleRequest(int userId, int conferenceId);
    Task<object> GetSpeakerRoleRequests();
    Task<bool> AcceptSpeakerRoleRequest(int userId, int conferenceId);
    Task<bool> RefuseSpeakerRoleRequest(int userId, int conferenceId);
}
