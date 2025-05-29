using System.Net;
using System.Text.Json;

namespace FinanceDashboard.Middlewares;

public class ExceptionMiddleware(RequestDelegate next,
 ILogger<ExceptionMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unhandled exception : {e.Message}");

            context.Response.StatusCode = (int)(HttpStatusCode.InternalServerError);
            context.Response.ContentType = "application/json";
            var response = new
            {
                status = 500,
                message = "An unexpected error occurred.",
                error = e.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}