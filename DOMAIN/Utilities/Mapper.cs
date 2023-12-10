using DOMAIN.DTOs;
using DOMAIN.Models;

namespace DOMAIN.Utilities;

public static class Mapper
{
    public static UserDTO ToUserDTO(this User user, IEnumerable<RoleDTO> roles) =>
        new UserDTO()
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Roles = roles
        };
}
