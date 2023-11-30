using DOMAIN.Requests;
using DOMAIN.Responses;
using DOMAIN.Utilities;

namespace APPLICATION.Contracts;

public interface IAuthService
{
    Task<Response> Register(RegisterRequest payload);
    Task<Response<AuthResponse>> Login(LoginRequest payload);
}
