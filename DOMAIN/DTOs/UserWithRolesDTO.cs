namespace DOMAIN.DTOs;

public class UserWithRolesDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IEnumerable<RoleDTO> Roles { get; set; } = Enumerable.Empty<RoleDTO>();
}
