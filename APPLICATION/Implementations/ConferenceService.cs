using APPLICATION.Contracts;
using DOMAIN.DTOs;
using DOMAIN.Models;
using DOMAIN.Utilities;
using PERSISTANCE.Contracts;

namespace APPLICATION.Implementations;

public class ConferenceService : IConferenceService
{
    private readonly IConferenceRepository _conferenceRepository;
    public ConferenceService(IConferenceRepository conferenceRepository) => _conferenceRepository = conferenceRepository;
    public async Task<Response> CreateConference(ConferenceDTO payload)
    {
        var response = new Response();

        var validationResult = new ConferenceDTOValidator().Validate(payload);

        if (!validationResult.IsValid)
        {
            response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            response.Message = "Invalid data!";
            return response;
        }

        await _conferenceRepository.CreateConference(payload);

        response.IsSucces = true;
        response.Message = "Conference created!";
        return response;
    }

    public async Task<Conference?> GetConference(int id) => await _conferenceRepository.GetConference(id);

    public async Task<IEnumerable<Conference>> GetConferences() => await _conferenceRepository.GetConferences();
}
