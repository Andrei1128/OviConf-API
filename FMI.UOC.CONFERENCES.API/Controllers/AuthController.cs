using APPLICATION.Contracts;
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

        if (response.IsSucces)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<Response<AuthResponse>>> Login([FromBody] LoginRequest payload)
    {
        var response = await _authService.Login(payload);

        if (response.IsSucces)
            return Ok(response);

        return BadRequest(response);
    }
}
