using DigitalRSVP.Core.Models;
using DigitalRSVP.Core.Services;
using DigitalRSVP.WAPI.Handlers.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DigitalRSVP.WAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : Controller
    {
        private readonly ILogger<EventController> _logger;

        private readonly IEventService _eventService;

        public EventController(ILogger<EventController> logger,
            IEventService eventService)
        {
            this._logger = logger;
            this._eventService = eventService;
        }

        [HttpGet("id={eventId}")]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(Event))]
        public async Task<IActionResult> GetEventById(string eventId)
        {
            Guid requestId = Guid.NewGuid();
            _logger.LogInformation($"[Request ID: {requestId}] Endpoint called @ {this.HttpContext.Request.Path} from {this.HttpContext.Connection.RemoteIpAddress}");
            try
            {
                if (Guid.TryParse(eventId, out Guid id))
                {
                    Event eventObj = await _eventService.GetEventByIdAsync(id);
                    if (eventObj != null)
                    {
                        return new JsonResult(eventObj);
                    }
                    else
                    {
                        return new StatusCodeResult(204);
                    }
                }
                else
                {
                    throw new InvalidDataException($"Input could not be parsed into a Guid...");
                }
            }
            catch (Exception exc)
            {
                using (ExceptionHandler excHandler = new ExceptionHandler())
                {
                    return await excHandler.HandleExceptionAndReturnHTTPStatusCode<EventController>(exc, _logger, requestId, this.HttpContext);
                }
            }
        }
    }
}
