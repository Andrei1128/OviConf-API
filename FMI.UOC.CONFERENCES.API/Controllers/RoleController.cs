using APPLICATION.Contracts;
using DOMAIN.Models;
using DOMAIN.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService) => _roleService = roleService;

        #region REQUEST_ROLE
        [Authorize(Policy = IdentityData.User)]
        [HttpPost("RequestHelperRole")]
        public async Task<ActionResult<Response>> RequestHelperRole([FromBody] int userId)
        {
            var response = await _roleService.RequestRole(userId, IdentityData.Helper);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Policy = IdentityData.User)]
        [HttpPost("RequestManagerRole")]
        public async Task<ActionResult<Response>> RequestManagerRole([FromBody] int userId, int conferenceId)
        {
            var response = await _roleService.RequestRole(userId, IdentityData.Manager, conferenceId);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Policy = IdentityData.User)]
        [HttpPost("RequestSpeakerRole")]
        public async Task<ActionResult<Response>> RequestSpeakerRole([FromBody] int userId, int conferenceId)
        {
            var response = await _roleService.RequestRole(userId, IdentityData.Speaker, conferenceId);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }
        #endregion

        #region GET_ROLE_REQUESTS
        [Authorize(Policy = IdentityData.Admin)]
        [HttpGet("GetHelperRoleRequests")]
        public async Task<ActionResult<IEnumerable<RoleRequest>>> GetHelperRoleRequests() => Ok(await _roleService.GetRoleRequests(IdentityData.Helper));

        [Authorize(Policy = IdentityData.Helper)]
        [HttpGet("GetManagerRoleRequests")]
        public async Task<ActionResult<IEnumerable<RoleRequest>>> GetManagerRoleRequests([FromBody] int conferenceId) => Ok(await _roleService.GetRoleRequests(IdentityData.Manager, conferenceId));

        [Authorize(Policy = IdentityData.Manager)]
        [HttpGet("GetSpeakerRoleRequests")]
        public async Task<ActionResult<IEnumerable<RoleRequest>>> GetSpeakerRoleRequests([FromBody] int conferenceId)
        {
            if (!IsConferenceAuthorized(conferenceId, ConferenceIdsClaim.Manager))
                return Unauthorized("You are not manager in this conference!");

            return Ok(await _roleService.GetRoleRequests(IdentityData.Speaker, conferenceId));
        }
        #endregion

        #region ACCEPT_ROLE_REQUEST
        [Authorize(Policy = IdentityData.Admin)]
        [HttpPost("AcceptHelperRoleRequest")]
        public async Task<ActionResult<Response>> AcceptHelperRoleRequest([FromBody] int userId)
        {
            var response = await _roleService.AcceptRoleRequest(userId, IdentityData.Helper);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Policy = IdentityData.Helper)]
        [HttpPost("AcceptManagerRoleRequest")]
        public async Task<ActionResult<Response>> AcceptManagerRoleRequest([FromBody] int userId, int conferenceId)
        {
            var response = await _roleService.AcceptRoleRequest(userId, IdentityData.Manager, conferenceId);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Policy = IdentityData.Manager)]
        [HttpPost("AcceptSpeakerRoleRequest")]
        public async Task<ActionResult<Response>> AcceptSpeakerRoleRequest([FromBody] int userId, int conferenceId)
        {
            if (!IsConferenceAuthorized(conferenceId, ConferenceIdsClaim.Manager))
                return Unauthorized("You are not manager in this conference!");

            var response = await _roleService.AcceptRoleRequest(userId, IdentityData.Speaker, conferenceId);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }
        #endregion

        #region REFUSE_ROLE_REQUEST
        [Authorize(Policy = IdentityData.Admin)]
        [HttpPost("RefuseHelperRoleRequest")]
        public async Task<ActionResult<Response>> RefuseHelperRoleRequest([FromBody] int userId)
        {
            var response = await _roleService.RefuseRoleRequest(userId, IdentityData.Helper);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Policy = IdentityData.Helper)]
        [HttpPost("RefuseManagerRoleRequest")]
        public async Task<ActionResult<Response>> RefuseManagerRoleRequest([FromBody] int userId, int conferenceId)
        {
            var response = await _roleService.RefuseRoleRequest(userId, IdentityData.Manager, conferenceId);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Policy = IdentityData.Manager)]
        [HttpPost("RefuseSpeakerRoleRequest")]
        public async Task<ActionResult<Response>> RefuseSpeakerRoleRequest([FromBody] int userId, int conferenceId)
        {
            if (!IsConferenceAuthorized(conferenceId, ConferenceIdsClaim.Manager))
                return Unauthorized("You are not manager in this conference!");

            var response = await _roleService.RefuseRoleRequest(userId, IdentityData.Speaker, conferenceId);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }
        #endregion

        private bool IsConferenceAuthorized(int conferenceId, string claimName)
        {
            var claims = HttpContext.User.Claims;
            var confIds = claims.Single(c => c.Type == claimName).Value.Split(",").ToList();
            return confIds.Contains(conferenceId.ToString());
        }
    }
}
