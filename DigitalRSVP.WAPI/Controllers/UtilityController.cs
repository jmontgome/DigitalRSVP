using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DigitalRSVP.WAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtilityController
    {
        private readonly ILogger<UtilityController> _logger;

        public UtilityController(ILogger<UtilityController> logger)
        {
            _logger = logger;
        }

        [HttpGet("NewGuid")]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNewGuid()
        {
            return new JsonResult(Guid.NewGuid());
        }
    }
}
