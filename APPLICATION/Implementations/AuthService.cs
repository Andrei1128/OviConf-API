﻿using APPLICATION.Contracts;
using DOMAIN.Requests;
using DOMAIN.Responses;
using DOMAIN.Utilities;
using Microsoft.Extensions.Configuration;
using PERSISTANCE.Contracts;
using BC = BCrypt.Net.BCrypt;

namespace APPLICATION.Implementations;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly IAuthRepository _authRepository;
    private readonly IJwtService _jwtService;
    public AuthService(IConfiguration config, IAuthRepository authRepository, IJwtService jwtService)
    {
        _config = config;
        _authRepository = authRepository;
        _jwtService = jwtService;
    }
    public async Task<Response> Register(RegisterRequest payload)
    {
        var response = new Response();

        if (payload.Password != payload.RePassword)
        {
            response.Message = "Passwords does not match!";
            return response;
        }

        var hashedPassword = BC.HashPassword(payload.Password);

        if (await _authRepository.RegisterUser(payload.Email, hashedPassword))
        {
            response.IsSucces = true;
            response.Message = "Account created!";
            return response;
        }

        response.Message = "An account with this email already exists!";
        return response;
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
            user.Password = string.Empty;

            response.Data = new AuthResponse()
            {
                User = user,
                Jwt = _jwtService.GenerateToken(user)
            };
            response.IsSucces = true;
            response.Message = "Login succes!";

            return response;
        }

        response.Message = "Invalid Email or Password!";
        return response;
    }
}
