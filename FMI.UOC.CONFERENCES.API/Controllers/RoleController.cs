using API.Utilities;
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

        [Authorize(Policy = IdentityData.Helper)]
        [HttpGet("GetAllRoleRequests")]
        public async Task<ActionResult<IEnumerable<RoleRequest>>> GeAllRoleRequests() => Ok(await _roleService.GetAllRoleRequests());

        #region REQUEST_ROLE
        [Authorize(Policy = IdentityData.User)]
        [HttpPost("RequestHelperRole")]
        public async Task<ActionResult<Response>> RequestHelperRole()
        {
            var response = await _roleService.RequestRole(IdentityData.Helper);

            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Policy = IdentityData.User)]
        [HttpPost("RequestManagerRole")]
        public async Task<ActionResult<Response>> RequestManagerRole([FromBody] int conferenceId)
        {
            var response = await _roleService.RequestRole(IdentityData.Manager, conferenceId);

            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Policy = IdentityData.User)]
        [HttpPost("RequestSpeakerRole")]
        public async Task<ActionResult<Response>> RequestSpeakerRole([FromBody] int conferenceId)
        {
            var response = await _roleService.RequestRole(IdentityData.Speaker, conferenceId);

            if (response.IsSuccess)
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
        public async Task<ActionResult<IEnumerable<RoleRequest>>> GetManagerRoleRequests([FromQuery] int conferenceId) => Ok(await _roleService.GetRoleRequests(IdentityData.Manager, conferenceId));

        [ConferenceAuthorize(IdentityData.Manager)]
        [HttpGet("GetSpeakerRoleRequests")]
        public async Task<ActionResult<IEnumerable<RoleRequest>>> GetSpeakerRoleRequests([FromQuery] int conferenceId) => Ok(await _roleService.GetRoleRequests(IdentityData.Speaker, conferenceId));
        #endregion

        #region ACCEPT_ROLE_REQUEST
        [Authorize(Policy = IdentityData.Admin)]
        [HttpPost("AcceptHelperRoleRequest")]
        public async Task<ActionResult<Response>> AcceptHelperRoleRequest([FromBody] int requestId)
        {
            var response = await _roleService.AcceptRoleRequest(requestId);

            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Policy = IdentityData.Helper)]
        [HttpPost("AcceptManagerRoleRequest")]
        public async Task<ActionResult<Response>> AcceptManagerRoleRequest([FromBody] int requestId)
        {
            var response = await _roleService.AcceptRoleRequest(requestId);

            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [ConferenceAuthorize(IdentityData.Manager)]
        [HttpPost("AcceptSpeakerRoleRequest")]
        public async Task<ActionResult<Response>> AcceptSpeakerRoleRequest([FromBody] int requestId, int conferenceId)
        {

            var response = await _roleService.AcceptRoleRequest(requestId);

            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }
        #endregion

        #region REFUSE_ROLE_REQUEST
        [Authorize(Policy = IdentityData.Admin)]
        [HttpPost("RefuseHelperRoleRequest")]
        public async Task<ActionResult<Response>> RefuseHelperRoleRequest([FromBody] int requestId)
        {
            var response = await _roleService.RefuseRoleRequest(requestId);

            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Policy = IdentityData.Helper)]
        [HttpPost("RefuseManagerRoleRequest")]
        public async Task<ActionResult<Response>> RefuseManagerRoleRequest([FromBody] int requestId)
        {
            var response = await _roleService.RefuseRoleRequest(requestId);

            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [ConferenceAuthorize(IdentityData.Manager)]
        [HttpPost("RefuseSpeakerRoleRequest")]
        public async Task<ActionResult<Response>> RefuseSpeakerRoleRequest([FromBody] int requestId, int conferenceId)
        {
            var response = await _roleService.RefuseRoleRequest(requestId);

            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }
        #endregion
    }
}
