namespace DOMAIN.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public List<Role> Roles { get; set; } = new List<Role>();
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsActive { get; set; }
}
