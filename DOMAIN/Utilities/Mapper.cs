using DOMAIN.DTOs;
using DOMAIN.Models;

namespace DOMAIN.Utilities;

public static class Mapper
{
    public static UserWithRolesDTO ToUserWithRolesDTO(this User user, IEnumerable<RoleDTO> roles) =>
        new UserWithRolesDTO()
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Roles = roles
        };
}
