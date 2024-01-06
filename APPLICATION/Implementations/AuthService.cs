using APPLICATION.Contracts;
using DOMAIN.DTOs;
using DOMAIN.Requests;
using DOMAIN.Responses;
using DOMAIN.Utilities;
using PERSISTANCE.Contracts;
using System.Data.SqlClient;
using BC = BCrypt.Net.BCrypt;

namespace APPLICATION.Implementations;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtService _jwtService;
    public AuthService(IAuthRepository authRepository, IJwtService jwtService)
    {
        _authRepository = authRepository;
        _jwtService = jwtService;
    }

    public async Task<Response> Register(RegisterRequest payload)
    {
        var response = new Response();

        var validationResult = new RegisterRequestValidator().Validate(payload);

        if (!validationResult.IsValid)
        {
            response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            response.Message = "Invalid data!";
            return response;
        }

        var hashedPassword = BC.HashPassword(payload.Password);

        try
        {
            await _authRepository.RegisterUser(payload.Email, payload.Name, hashedPassword);

            response.IsSucces = true;
            response.Message = "Account created!";
            return response;
        }
        catch (SqlException ex)
        {
            if (ex.Number == SqlExceptionCodes.UNIQUE_CONSTRAINT_VIOLATION)
            {
                response.Message = "An account with this email already exists!";
                return response;
            }
            else throw;
        }
    }
    public async Task<Response<AuthResponse>> Login(LoginRequest payload)
    {
        var response = new Response<AuthResponse>();

        var user = await _authRepository.GetUserData(payload.Email);

        if (user is null)
        {
            response.Message = "Invalid Email or Password!";
            return response;
        }

        if (BC.Verify(payload.Password, user.Password))
        {
            UserWithRolesDTO userDTO = user.ToUserWithRolesDTO(await _authRepository.GetUserRoles(user.Id));

            response.Data = new AuthResponse()
            {
                User = userDTO,
                Jwt = _jwtService.GenerateToken(userDTO)
            };

            response.IsSucces = true;
            response.Message = "Login succes!";

            return response;
        }

        response.Message = "Invalid Email or Password!";
        return response;
    }
}
