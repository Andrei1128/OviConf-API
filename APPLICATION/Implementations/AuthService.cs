﻿using APPLICATION.Contracts;
using DOMAIN.DTOs;
using DOMAIN.Requests;
using DOMAIN.Responses;
using DOMAIN.Utilities;
using PERSISTANCE.Contracts;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
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

        if (!IsValidEmail(payload.Email))
        {
            response.Message = "Invalid email address!";
            return response;
        }

        if (payload.Password != payload.RePassword)
        {
            response.Message = "Passwords does not match!";
            return response;
        }

        if (!IsStrongPassword(payload.Password))
        {
            response.Message = "Password is too weak!";
            return response;
        }

        var hashedPassword = BC.HashPassword(payload.Password);

        try
        {
            await _authRepository.RegisterUser(payload.Email, hashedPassword);

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

    private bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(email);
    }

    private bool IsStrongPassword(string password)
    {
        string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(password);
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
            UserDTO userDTO = user.ToUserDTO(await _authRepository.GetUserRoles(user.Id));

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
