using APPLICATION.Contracts;
using DOMAIN.DTOs;
using DOMAIN.Requests;
using DOMAIN.Responses;
using DOMAIN.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService) => _authService = authService;

    [HttpPost("Register")]
    public async Task<ActionResult<Response>> Register([FromBody] RegisterRequest payload)
    {
        var response = await _authService.Register(payload);

        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<Response<AuthResponse>>> Login([FromBody] LoginRequest payload)
    {
        var response = await _authService.Login(payload);

        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpGet("GetUserInfo")]
    public async Task<ActionResult<Response<UserWithRolesDTO>>> GetUserInfo()
    {
        var response = await _authService.GetUserInfo();

        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPost("EditUser")]
    public async Task<ActionResult<Response>> EditUser(RegisterRequest payload)
    {
        var response = await _authService.EditUser(payload);

        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }
}
