using DOMAIN.DTOs;
using DOMAIN.Models;

namespace PERSISTANCE.Contracts;

public interface IConferenceRepository
{
    Task CreateConference(ConferenceDTO payload);
    Task<IEnumerable<Conference>> GetConferences();
    Task<Conference?> GetConference(int id);
    Task RegisterAtConference(int conferenceId, int userId);
    Task<IEnumerable<Conference>> GetMyConferences(int userId);
    Task<IEnumerable<UserDTO>> GetPeople(int conferenceId, string role);
    Task AddNavItem(NavItem navItem);
}
