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
    private readonly ThisUser _thisUser;
    public AuthService(IAuthRepository authRepository, IJwtService jwtService, ThisUser thisUser)
    {
        _authRepository = authRepository;
        _jwtService = jwtService;
        _thisUser = thisUser;
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

            response.IsSuccess = true;
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
    public async Task<Response> EditUser(RegisterRequest payload)
    {
        var response = new Response();

        var validationResult = new RegisterRequestValidator().Validate(payload);

        if (!validationResult.IsValid || _thisUser.Email != payload.Email)
        {
            response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            response.Message = "Invalid data!";
            return response;
        }

        var isSuccess = await _authRepository.EditUser(_thisUser.Id, payload.Name, payload.Password);

        if (isSuccess)
        {
            response.IsSuccess = true;
            response.Message = "Account details saved!";
            return response;
        }
        else
        {
            response.Message = "Could not save details!";
            return response;
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

            response.IsSuccess = true;
            response.Message = "Login succes!";

            return response;
        }

        response.Message = "Invalid Email or Password!";
        return response;
    }

    public async Task<Response<UserWithRolesDTO>> GetUserInfo()
    {
        var response = new Response<UserWithRolesDTO>();

        var user = await _authRepository.GetUserInfo(_thisUser.Id);

        if (user is not null)
        {
            user.Roles = await _authRepository.GetUserRoles(user.Id);

            response.Data = user;
            response.IsSuccess = true;
            response.Message = "Success!";
            return response;
        }
        else
        {
            response.Message = "Could not find user!";
            return response;
        }
    }
}
