namespace DOMAIN.Models;

public class NavItem
{
    public int Id { get; set; }
    public int ConferenceId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int? Order { get; set; }
    public bool IsActive { get; set; }
}
