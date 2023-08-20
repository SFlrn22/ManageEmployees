using ManageEmployees.API.Models;
using ManageEmployees.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace ManageEmployees.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomValidationProblemDetails problem = new();

            switch (ex)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomValidationProblemDetails
                    {
                        Title = badRequestException.Message,
                        Status = (int)statusCode,
                        Type = nameof(BadRequestException),
                        Detail = badRequestException.InnerException?.Message,
                        Errors = badRequestException.ValidationErrors
                    };
                    break;
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    problem = new CustomValidationProblemDetails
                    {
                        Title = notFoundException.Message,
                        Status = (int)statusCode,
                        Type = nameof(NotFoundException),
                        Detail = notFoundException.InnerException?.Message,
                    };
                    break;
                default:
                    problem = new CustomValidationProblemDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.InternalServerError),
                        Detail = ex.StackTrace,
                    };
                    break;
            }

            httpContext.Response.StatusCode = (int)statusCode;
            var logMessage = JsonSerializer.Serialize(problem);
            _logger.LogError(ex, logMessage);
            await httpContext.Response.WriteAsJsonAsync(problem);

        }
    }
}
