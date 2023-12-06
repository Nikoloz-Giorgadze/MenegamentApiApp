using ManagementApi.Infrastructure.Exceptions;
using System.Net;
using System.Text.Json;
using KeyNotFoundException = ManagementApi.Infrastructure.Exceptions.KeyNotFoundException;
using NotImplementedException = ManagementApi.Infrastructure.Exceptions.NotImplementedException;


namespace ManagementApi.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
        private static Dictionary<Type, HttpStatusCode> _exceptionStatusCodeMapping;
        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        static GlobalExceptionHandlingMiddleware()
        {
            _exceptionStatusCodeMapping = new Dictionary<Type, HttpStatusCode>()
        {
            {typeof(BadRequestException), HttpStatusCode.BadRequest },
            {typeof(NotFoundException),HttpStatusCode.NotFound},
            {typeof(NotImplementedException), HttpStatusCode.NotImplemented},
            {typeof(KeyNotFoundException), HttpStatusCode.NotFound},
            {typeof(UnauthorizedAccessException),HttpStatusCode.Unauthorized},
        };
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode status;
            var exceptiontype = ex.GetType();

            if (_exceptionStatusCodeMapping.ContainsKey(exceptiontype))
            {
                status = _exceptionStatusCodeMapping[exceptiontype];
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
            }

            string message = ex.Message;
            string? stackTrace = ex.StackTrace;

            _logger.LogError(ex, ex.Message);

            var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(exceptionResult);
        }
    }
}