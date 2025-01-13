using Microsoft.AspNetCore.Mvc;

namespace DigitalRSVP.WAPI.Handlers.Exceptions
{
    public interface IExceptionHandler
    {
        Task<StatusCodeResult> HandleExceptionAndReturnHTTPStatusCode<T>(Exception exc, ILogger<T> logger, Guid requestId, HttpContext httpContext);
    }
}
