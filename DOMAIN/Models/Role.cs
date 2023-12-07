namespace DOMAIN.Models;

public class Role
{
    public string UserName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int? ConferenceId { get; set; }
}
