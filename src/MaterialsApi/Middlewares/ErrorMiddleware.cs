using MaterialsApi.Exceptions;
using System.Net;

namespace MaterialsApi.Middlewares
{
    public class ErrorMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public ErrorMiddleware(ILogger<ErrorMiddleware> logger)
            => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try { await next.Invoke(context); }
            catch (NotFoundException notFoundException)
            {
                await HandleExceptionAsync(context, notFoundException, HttpStatusCode.NotFound);
            }
            catch (BadRequestException badRequestException)
            {
                await HandleExceptionAsync(context, badRequestException, HttpStatusCode.BadRequest);
            }
            catch (UnauthorizedAccessException unauthorizeAccessException)
            {
                await HandleExceptionAsync(context, unauthorizeAccessException, HttpStatusCode.Unauthorized);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {
            _logger.LogError($"errror message:{exception.Message} date:{DateTime.Now}");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(new { Error = exception.Message });
        }
    }
}