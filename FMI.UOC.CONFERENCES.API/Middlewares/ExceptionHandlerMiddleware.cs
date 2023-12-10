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
            Log.Fatal(ex, "Unexpected exception!");

            context.Response.Clear();
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("Something went wrong!");
        }
    }
}
