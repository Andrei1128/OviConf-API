using DOMAIN.Models;

namespace PERSISTANCE.Contracts;

public interface IRoleRepository
{
    Task RequestRole(int userId, string role, int? conferenceId = null);
    Task<IEnumerable<RoleRequest>> GetRoleRequests(string role, int? conferenceId = null);
    Task AcceptRoleRequest(int userId, string role, int? conferenceId = null);
    Task RefuseRoleRequest(int userId, string role, int? conferenceId = null);
}
