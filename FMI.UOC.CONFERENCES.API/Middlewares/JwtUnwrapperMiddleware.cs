using DOMAIN.Utilities;

namespace API.Middlewares;

public class JwtUnwrapperMiddleware : IMiddleware
{
    private readonly ThisUser _thisUser;
    public JwtUnwrapperMiddleware(ThisUser thisUser) => _thisUser = thisUser;
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var claims = context.User.Claims;

        if (claims.Any())
        {
            _thisUser.Id = Convert.ToInt32(claims.Single(c => c.Type == "Id").Value);
            _thisUser.Name = claims.Single(c => c.Type == "Name").Value;
            _thisUser.Email = claims.Single(c => c.Type == "Email").Value;
        }

        await next(context);
    }
}
