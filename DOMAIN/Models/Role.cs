namespace DOMAIN.Models;

public class Role
{
    public int UserId { get; set; }
    public int? ConferenceId { get; set; }
    public string RoleName { get; set; } = string.Empty;
}
