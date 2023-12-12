using DOMAIN.DTOs;
using DOMAIN.Models;

namespace PERSISTANCE.Contracts;

public interface IConferenceRepository
{
    Task CreateConference(ConferenceDTO payload);
    Task<IEnumerable<Conference>> GetConferences();
    Task<Conference?> GetConference(int id);
}
