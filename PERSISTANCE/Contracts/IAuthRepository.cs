using DOMAIN.DTOs;
using DOMAIN.Models;

namespace PERSISTANCE.Contracts;

public interface IAuthRepository
{
    Task<User?> GetUserData(string email);
    Task RegisterUser(string email, string name, string password);
    Task<IEnumerable<RoleDTO>> GetUserRoles(int userId);
}
