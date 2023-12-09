using DOMAIN.Models;

namespace PERSISTANCE.Contracts;

public interface IAuthRepository
{
    Task<User?> GetUserData(string email);
    Task RegisterUser(string email, string password);
    Task<IEnumerable<Role>> GetUserRoles(int userId);
}
