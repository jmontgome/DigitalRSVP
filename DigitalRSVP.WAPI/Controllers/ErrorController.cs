using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DigitalRSVP.WAPI.Controllers
{
    public class ErrorController
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

        }
    }
}
