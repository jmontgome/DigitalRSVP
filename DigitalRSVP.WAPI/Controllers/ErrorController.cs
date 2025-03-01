using DigitalRSVP.Core.Models;
using DigitalRSVP.WAPI.Handlers.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Mime;

namespace DigitalRSVP.WAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this._logger = logger;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> SubmitError(object error)
        {
            Guid requestId = Guid.NewGuid();
            _logger.LogInformation($"[Request ID: {requestId}] Endpoint called @ [POST]{this.HttpContext.Request.Path} from {this.HttpContext.Connection.RemoteIpAddress}");
            try
            {
                if (error != null)
                {
                    Error? parsedError = JsonConvert.DeserializeObject<Error>(error.ToString()!);
                    if (parsedError != null)
                    {
                        _logger.LogError($"[Request ID: {requestId}] Error: {parsedError.ToString()}");
                    }
                }
                throw new InvalidDataException($"Something went wrong with the data given. \r\nContent: {error.ToString()}");
            }
            catch (Exception exc)
            {
                using (ExceptionHandler excHandler = new ExceptionHandler())
                {
                    return await excHandler.HandleExceptionAndReturnHTTPStatusCode<ErrorController>(exc, _logger, requestId, this.HttpContext);
                }
            }
        }
    }
}
