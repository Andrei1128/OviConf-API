using APPLICATION.Contracts;
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

        [Authorize(Policy = IdentityData.User)]
        [HttpPost("RequestHelperRole")]
        public async Task<IActionResult> RequestHelperRole([FromBody] int userId)
        {
            var response = await _roleService.RequestRole(userId, IdentityData.Helper);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Policy = IdentityData.User)]
        [HttpPost("RequestManagerRole")]
        public async Task<IActionResult> RequestManagerRole([FromBody] int userId, int conferenceId)
        {
            var response = await _roleService.RequestRole(userId, IdentityData.Manager, conferenceId);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Policy = IdentityData.User)]
        [HttpPost("RequestSpeakerRole")]
        public async Task<IActionResult> RequestSpeakerRole([FromBody] int userId, int conferenceId)
        {
            var response = await _roleService.RequestRole(userId, IdentityData.Speaker, conferenceId);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Policy = IdentityData.Admin)]
        [HttpGet("GetHelperRoleRequests")]
        public async Task<IActionResult> GetHelperRoleRequests() => Ok(await _roleService.GetHelperRoleRequests());

        [Authorize(Policy = IdentityData.Admin)]
        [HttpPost("AcceptHelperRoleRequest")]
        public async Task<IActionResult> AcceptHelperRoleRequest([FromBody] int userId)
        {
            var response = await _roleService.AcceptHelperRoleRequest(userId);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Policy = IdentityData.Admin)]
        [HttpPost("RefuseHelperRoleRequest")]
        public async Task<IActionResult> RefuseHelperRoleRequest([FromBody] int userId)
        {
            var response = await _roleService.RefuseHelperRoleRequest(userId);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Policy = IdentityData.Helper)]
        [HttpGet("GetManagerRoleRequests")]
        public async Task<IActionResult> GetManagerRoleRequests() => Ok(await _roleService.GetManagerRoleRequests());

        [Authorize(Policy = IdentityData.Helper)]
        [HttpPost("AcceptManagerRoleRequest")]
        public async Task<IActionResult> AcceptManagerRoleRequest([FromBody] int userId, int conferenceId)
        {
            var response = await _roleService.AcceptManagerRoleRequest(userId, conferenceId);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Policy = IdentityData.Helper)]
        [HttpPost("RefuseManagerRoleRequest")]
        public async Task<IActionResult> RefuseManagerRoleRequest([FromBody] int userId, int conferenceId)
        {
            var response = await _roleService.RefuseManagerRoleRequest(userId, conferenceId);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Policy = IdentityData.Manager)]
        [HttpGet("GetSpeakerRoleRequests")]
        public async Task<IActionResult> GetSpeakerRoleRequests() => Ok(await _roleService.GetSpeakerRoleRequests());

        [Authorize(Policy = IdentityData.Manager)]
        [HttpPost("AcceptSpeakerRoleRequest")]
        public async Task<IActionResult> AcceptSpeakerRoleRequest([FromBody] int userId, int conferenceId)
        {
            var response = await _roleService.AcceptSpeakerRoleRequest(userId, conferenceId);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize(Policy = IdentityData.Manager)]
        [HttpPost("RefuseSpeakerRoleRequest")]
        public async Task<IActionResult> RefuseSpeakerRoleRequest([FromBody] int userId, int conferenceId)
        {
            var response = await _roleService.RefuseSpeakerRoleRequest(userId, conferenceId);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
