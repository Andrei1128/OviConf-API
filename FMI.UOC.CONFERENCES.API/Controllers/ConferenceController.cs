using APPLICATION.Contracts;
using DOMAIN.DTOs;
using DOMAIN.Models;
using DOMAIN.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferenceController : ControllerBase
    {
        private readonly IConferenceService _conferenceService;
        public ConferenceController(IConferenceService conferenceService) => _conferenceService = conferenceService;

        [HttpPost("Create")]
        [Authorize(Policy = IdentityData.Helper)]
        public async Task<ActionResult<Response>> CreateConference([FromBody] ConferenceDTO payload)
        {
            var response = await _conferenceService.CreateConference(payload);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("RegisterAtConference")]
        [Authorize(Policy = IdentityData.User)]
        public async Task<ActionResult<Response>> RegisterAtConference([FromBody] int conferenceId)
        {
            var response = await _conferenceService.RegisterAtConference(conferenceId);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("AddNavItem")]
        [Authorize(Policy = IdentityData.User)]
        public async Task<ActionResult<Response>> AddNavItem([FromBody] NavItem navItem)
        {
            var response = await _conferenceService.AddNavItem(navItem);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("GetMyConferences")]
        [Authorize(Policy = IdentityData.User)]
        public async Task<ActionResult<IEnumerable<Conference>>> GetMyConferences() => Ok(await _conferenceService.GetMyConferences());

        [HttpGet("GetConferences")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Conference>>> GetConferences() => Ok(await _conferenceService.GetConferences());

        [HttpGet("GetConference")]
        [AllowAnonymous]
        public async Task<ActionResult<Conference?>> GetConference([FromQuery] int id) => Ok(await _conferenceService.GetConference(id));
        [HttpGet("GetParticipants")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetParticipants([FromQuery] int conferenceId) => Ok(await _conferenceService.GetPeople(conferenceId, IdentityData.User));
        [HttpGet("GetSpeakers")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetSpeakers([FromQuery] int conferenceId) => Ok(await _conferenceService.GetPeople(conferenceId, IdentityData.Speaker));
        [HttpGet("GetManagers")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetManagers([FromQuery] int conferenceId) => Ok(await _conferenceService.GetPeople(conferenceId, IdentityData.Manager));
    }
}
