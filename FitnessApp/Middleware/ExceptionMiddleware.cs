using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Middleware
{
    internal class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Request was canceled by the client or platform timeout.");

                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 499;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        error = "Request was canceled",
                        traceId = context.TraceIdentifier
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred while processing request: {TraceId}", context.TraceIdentifier);

                if (context.Response.HasStarted)
                    throw; // too late to modify response

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/problem+json";

                var problem = new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Status = StatusCodes.Status500InternalServerError,
                    Instance = context.TraceIdentifier,
                    Detail = "An unexpected error occurred."
                };

                await context.Response.WriteAsJsonAsync(problem);
            }
        }
    }
}