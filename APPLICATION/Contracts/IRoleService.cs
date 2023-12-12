using DOMAIN.Models;
using DOMAIN.Utilities;

namespace APPLICATION.Contracts;

public interface IRoleService
{
    Task<Response> RequestRole(string role, int? conferenceId = null);
    Task<IEnumerable<RoleRequest>> GetRoleRequests(string role, int? conferenceId = null);
    Task<Response> AcceptRoleRequest(int requestId);
    Task<Response> RefuseRoleRequest(int requestId);
}
