using System.Text.Json;
using Store.S_02.APIs.Error;

namespace Store.S_02.APIs.MiddleWears;

public class ExceptionMiddlewear
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddlewear> _looger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddlewear(RequestDelegate next, ILogger<ExceptionMiddlewear> looger, IHostEnvironment env)
    {
        _next = next;
        _looger = looger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception e)
        {
            _looger.LogError(e, e.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            if (e.StackTrace != null)
            {
                var response = _env.IsDevelopment()
                    ? new ApisExeptionResponse(StatusCodes.Status500InternalServerError, e.StackTrace.ToString(), e.Message)
                    : new ApisExeptionResponse(StatusCodes.Status500InternalServerError);

                var json = JsonSerializer.Serialize(response);
         
                await context.Response.WriteAsync(json);
            }
        }
    }
}