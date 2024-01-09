using DOMAIN.DTOs;
using DOMAIN.Models;
using DOMAIN.Utilities;

namespace APPLICATION.Contracts;

public interface IConferenceService
{
    public Task<Response<int>> CreateConference(Conference conferenceRequest);
    public Task<IEnumerable<Conference>> GetConferences();
    public Task<Conference?> GetConference(int id);
    public Task<Response> RegisterAtConference(int conferenceId);
    public Task<IEnumerable<Conference>> GetMyConferences();
    public Task<IEnumerable<UserDTO>> GetConferencePeople(int conferenceId, string role);
    public Task<Response> AddNavItem(NavItem navItem);
    public Task<Response> UpdateNavItem(NavItem navItem);
    public Task<Response> UpdateConference(Conference payload);
    public Task<IEnumerable<NavTitleDTO>> GetNavItems(int conferenceId);
    public Task<string> GetNavItemContent(int navItemId);
}
