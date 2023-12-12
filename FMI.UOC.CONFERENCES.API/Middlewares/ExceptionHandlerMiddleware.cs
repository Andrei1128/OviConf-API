using DOMAIN.Utilities;
using Serilog;

namespace API.Middlewares;

public class ExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.Clear();
            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = 500;

            if (ex is CustomException)
            {
                await context.Response.WriteAsync(ex.Message);
            }
            else
            {
                Log.Fatal(ex, "Unexpected exception!");

                await context.Response.WriteAsync("Something went wrong!");
            }
        }
    }
}
