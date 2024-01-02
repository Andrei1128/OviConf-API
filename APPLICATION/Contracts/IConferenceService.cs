using DOMAIN.DTOs;
using DOMAIN.Models;
using DOMAIN.Utilities;

namespace APPLICATION.Contracts;

public interface IConferenceService
{
    public Task<Response> CreateConference(ConferenceDTO conferenceRequest);
    public Task<IEnumerable<Conference>> GetConferences();
    public Task<Conference?> GetConference(int id);
    public Task<Response> RegisterAtConference(int conferenceId);
    public Task<IEnumerable<Conference>> GetMyConferences();
    public Task<IEnumerable<UserDTO>> GetPeople(int conferenceId, string role);
    public Task<Response> AddNavItem(NavItem navItem);
}
