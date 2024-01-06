using APPLICATION.Contracts;
using DOMAIN.DTOs;
using DOMAIN.Models;
using DOMAIN.Utilities;
using PERSISTANCE.Contracts;
using System.Data;
using System.Data.SqlClient;

namespace APPLICATION.Implementations;

public class ConferenceService : IConferenceService
{
    private readonly IConferenceRepository _conferenceRepository;
    private readonly ThisUser _thisUser;
    public ConferenceService(IConferenceRepository conferenceRepository, ThisUser thisUser)
    {
        _conferenceRepository = conferenceRepository;
        _thisUser = thisUser;
    }

    public async Task<Conference?> GetConference(int id) => await _conferenceRepository.GetConference(id);
    public async Task<IEnumerable<Conference>> GetConferences() => await _conferenceRepository.GetConferences();
    public async Task<IEnumerable<Conference>> GetMyConferences() => await _conferenceRepository.GetMyConferences(_thisUser.Id);

    public async Task<Response> CreateConference(Conference payload)
    {
        var response = new Response();

        var validationResult = new ConferenceValidator().Validate(payload);

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

    public async Task<Response> RegisterAtConference(int conferenceId)
    {
        var response = new Response();

        try
        {
            await _conferenceRepository.RegisterAtConference(conferenceId, _thisUser.Id);

            response.IsSucces = true;
            response.Message = "Registered successfully!!";
            return response;
        }
        catch (SqlException ex)
        {
            if (ex.Number == SqlExceptionCodes.UNIQUE_CONSTRAINT_VIOLATION)
            {
                response.Message = "You are already registered at this conference!";
                return response;
            }
            else throw;
        }
    }

    public async Task<IEnumerable<UserWithRolesDTO>> GetPeople(int conferenceId, string role) => await _conferenceRepository.GetPeople(conferenceId, role);

    public async Task<Response> AddNavItem(NavItem navItem)
    {
        var response = new Response();

        await _conferenceRepository.AddNavItem(navItem);

        response.IsSucces = true;
        response.Message = "Success!";
        return response;
    }

    public async Task<Response> UpdateNavItem(NavItem navItem)
    {
        var response = new Response();

        await _conferenceRepository.UpdateNavItem(navItem);

        response.IsSucces = true;
        response.Message = "Success!";
        return response;
    }

    public async Task<Response> UpdateConference(Conference payload)
    {
        var response = new Response();

        await _conferenceRepository.UpdateConference(payload);

        response.IsSucces = true;
        response.Message = "Success!";
        return response;
    }

    public async Task<IEnumerable<NavTitleDTO>> GetNavItems(int conferenceId) => await _conferenceRepository.GetNavItems(conferenceId);
    public async Task<string> GetNavItemContent(int navItemId) => await _conferenceRepository.GetNavItemContent(navItemId);
}
