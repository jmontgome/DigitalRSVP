using DigitalRSVP.Core.Models;
using DigitalRSVP.Core.Services;
using DigitalRSVP.WAPI.Handlers.Exceptions;
using DigitalRSVP.WAPI.Reporting;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DigitalRSVP.WAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtilityController : Controller
    {
        private readonly ILogger<UtilityController> _logger;
        private readonly IRSVPService _rsvpService;
        private readonly IEventService _eventService;
        private readonly IInvitationService _inviteService;

        public UtilityController(ILogger<UtilityController> logger,
            IRSVPService rsvpService, IEventService eventService,
            IInvitationService inviteService)
        {
            _logger = logger;
            _rsvpService = rsvpService;
            _eventService = eventService;
            _inviteService = inviteService;
        }

        [HttpGet("NewGuid")]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNewGuid()
        {
            return new JsonResult(Guid.NewGuid());
        }

        [HttpPost("Report={email}")]
        public async Task<IActionResult> TriggerReportSending(string email)
        {
            Guid requestId = Guid.NewGuid();
            _logger.LogInformation($"[Request ID: {requestId}] Endpoint called @ [GET]{this.HttpContext.Request.Path} from {this.HttpContext.Connection.RemoteIpAddress}");
            try
            {
                Event eventInServer = await this._eventService.GetEventByEmailAsync(email);
                if (eventInServer != null)
                {
                    IEnumerable<RSVP> rsvps = await this._rsvpService.GetRSVPsByEventIdAsync(eventInServer.Id);
                    IEnumerable<Invitation> invitations = await this._inviteService.GetInvitationsByEventIdAsync(eventInServer.Id);
                    using (RSVPReportFactory<UtilityController> reportFactory = new RSVPReportFactory<UtilityController>(this._logger, rsvps, invitations))
                    {
                        //Either do a CSV file attachment in email or send a report in the body of an email.
                        //using (MemoryStream memoryStream = reportFactory.GenerateCSVFile())
                        //{
                        //    //Write the reporting data down
                        //}
                    }
                }
            }
            catch (Exception exc)
            {
                using (ExceptionHandler excHandler = new ExceptionHandler())
                {
                    return await excHandler.HandleExceptionAndReturnHTTPStatusCode<UtilityController>(exc, _logger, requestId, this.HttpContext);
                }
            }
            return new StatusCodeResult(200);
        }
    }
}
