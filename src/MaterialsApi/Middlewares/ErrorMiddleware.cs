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
                await HandleExceptionAsync(context, badRequestException, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                _logger.LogError($"({DateTime.Now}) Unhandled Exception: {context.Request.Method}: {context.Request.Scheme}://{context.Request.Host}{context.Request.Path}\n\n{exception.Message}\n{exception}");
                await HandleExceptionAsync(context, exception, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(new { Error = exception.Message });
        }
    }
}