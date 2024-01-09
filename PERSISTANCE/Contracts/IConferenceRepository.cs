using DOMAIN.DTOs;
using DOMAIN.Models;

namespace PERSISTANCE.Contracts;

public interface IConferenceRepository
{
    Task<int> CreateConference(Conference payload);
    Task<IEnumerable<Conference>> GetConferences();
    Task<Conference?> GetConference(int id);
    Task RegisterAtConference(int conferenceId, int userId);
    Task<IEnumerable<Conference>> GetMyConferences(int userId);
    Task<IEnumerable<UserDTO>> GetConferencePeople(int conferenceId, string role);
    Task AddNavItem(NavItem navItem);
    Task UpdateNavItem(NavItem navItem);
    Task UpdateConference(Conference payload);
    Task<IEnumerable<NavTitleDTO>> GetNavItems(int conferenceId);
    Task<string> GetNavItemContent(int navItemId);
}
