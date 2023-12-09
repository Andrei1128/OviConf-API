namespace DOMAIN.Models;

public class RoleRequest
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string RequestedRole { get; set; } = string.Empty;
    public int? ConferenceId { get; set; }
    public DateTime RequestedAt { get; set; }
}
