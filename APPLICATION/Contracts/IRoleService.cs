using DOMAIN.Models;
using DOMAIN.Utilities;

namespace APPLICATION.Contracts;

public interface IRoleService
{
    Task<Response> RequestRole(int userId, string role, int? conferenceId = null);
    Task<IEnumerable<RoleRequest>> GetRoleRequests(string role, int? conferenceId = null);
    Task<Response> AcceptRoleRequest(int userId, string role, int? conferenceId = null);
    Task<Response> RefuseRoleRequest(int userId, string role, int? conferenceId = null);
}
