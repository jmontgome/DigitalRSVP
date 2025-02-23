using DigitalRSVP.Core.Models;
using DigitalRSVP.Core.Services;
using DigitalRSVP.WAPI.Handlers.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Mime;

namespace DigitalRSVP.WAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RSVPController : Controller
    {
        private readonly ILogger<RSVPController> _logger;

        private readonly IRSVPService _rsvpService;
        private readonly IInvitationService _invitationService;

        public RSVPController(ILogger<RSVPController> logger, IRSVPService rsvpService,
            IInvitationService invitationService)
        {
            _logger = logger;
            _rsvpService = rsvpService;
            _invitationService = invitationService;
        }

        [HttpGet("id={rsvpId}")]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(RSVP))]
        public async Task<IActionResult> GetRSVP(string rsvpId)
        {
            Guid requestId = Guid.NewGuid();
            _logger.LogInformation($"[Request ID: {requestId}] Endpoint called @ {this.HttpContext.Request.Path} from {this.HttpContext.Connection.RemoteIpAddress}");
            try
            {
                if (Guid.TryParse(rsvpId, out Guid id))
                {
                    RSVP rsvp = await _rsvpService.GetRSVPAsync(id);
                    if (rsvp != null)
                    {
                        return new JsonResult(rsvp);
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
                    return await excHandler.HandleExceptionAndReturnHTTPStatusCode<RSVPController>(exc, _logger, requestId, this.HttpContext);
                }
            }
        }

        [HttpGet("invitation={invId}")]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(RSVP))]
        public async Task<IActionResult> GetRSVPByInvitee(string invId)
        {
            Guid requestId = Guid.NewGuid();
            _logger.LogInformation($"[Request ID: {requestId}] Endpoint called @ {this.HttpContext.Request.Path} from {this.HttpContext.Connection.RemoteIpAddress}");
            try
            {
                if (Guid.TryParse(invId, out Guid id))
                {
                    RSVP rsvp = await _rsvpService.GetRSVPByInviteeAsync(id);
                    if (rsvp != null)
                    {
                        return new JsonResult(rsvp);
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
                    return await excHandler.HandleExceptionAndReturnHTTPStatusCode<RSVPController>(exc, _logger, requestId, this.HttpContext);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRSVP()
        {
            Guid requestId = Guid.NewGuid();
            _logger.LogInformation($"[Request ID: {requestId}] Endpoint called @ {this.HttpContext.Request.Path} from {this.HttpContext.Connection.RemoteIpAddress}");
            try
            {
                string bodyText;
                using (StreamReader bodyReader = new StreamReader(HttpContext.Request.Body))
                {
                    bodyText = await bodyReader.ReadToEndAsync();
                }
                if (!string.IsNullOrWhiteSpace(bodyText))
                {
                    RSVP? submittedRSVP = JsonConvert.DeserializeObject<RSVP>(bodyText);
                    if (submittedRSVP != null)
                    {
                        if (await _invitationService.InvitationAuthorizedAsync(submittedRSVP.InviteeId))
                        {
                            RSVP check = await _rsvpService.GetRSVPByInviteeAsync(submittedRSVP.InviteeId);
                            if (check == null)
                            {
                                await _rsvpService.SubmitRSVPAsync(submittedRSVP);
                                return new StatusCodeResult(200);
                            }
                            else
                            {
                                throw new InvalidDataException($"User submitted an RSVP that already exists, Invitation ID: {submittedRSVP.InviteeId}.");
                            }
                        }
                        else
                        {
                            throw new UnauthorizedAccessException($"User gave an invalid invitation ID, {submittedRSVP.InviteeId}.");
                        }
                    }
                    throw new InvalidDataException($"Something went wrong with the data given. \r\n\tContent: {bodyText}");
                }
                throw new InvalidDataException($"No Content was provided.");
            }
            catch (Exception exc)
            {
                using (ExceptionHandler excHandler = new ExceptionHandler())
                {
                    return await excHandler.HandleExceptionAndReturnHTTPStatusCode<RSVPController>(exc, _logger, requestId, this.HttpContext);
                }
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditRSVP()
        {
            Guid requestId = Guid.NewGuid();
            _logger.LogInformation($"[Request ID: {requestId}] Endpoint called @ {this.HttpContext.Request.Path} from {this.HttpContext.Connection.RemoteIpAddress}");
            try
            {
                string bodyText;
                using (StreamReader bodyReader = new StreamReader(HttpContext.Request.Body))
                {
                    bodyText = bodyReader.ReadToEnd();
                }
                if (!string.IsNullOrWhiteSpace(bodyText))
                {
                    RSVP? submittedRSVP = JsonConvert.DeserializeObject<RSVP>(bodyText);
                    if (submittedRSVP != null)
                    {
                        if (await _invitationService.InvitationAuthorizedAsync(submittedRSVP.InviteeId))
                        {
                            RSVP check = await _rsvpService.GetRSVPByInviteeAsync(submittedRSVP.InviteeId);
                            if (check != null)
                            {
                                await _rsvpService.EditRSVPAsync(submittedRSVP);
                                return new StatusCodeResult(200);
                            }
                            else
                            {
                                throw new InvalidDataException($"User attempted to edit an RSVP that does not exist, Invitation ID: {submittedRSVP.InviteeId}.");
                            }
                        }
                        else
                        {
                            throw new UnauthorizedAccessException($"User gave an invalid invitation ID, Invitation ID: {submittedRSVP.InviteeId}.");
                        }
                    }
                    throw new InvalidDataException($"Something went wrong with the data given. \r\n\tContent: {bodyText}");
                }
                throw new InvalidDataException($"No Content was provided.");
            }
            catch (Exception exc)
            {
                using (ExceptionHandler excHandler = new ExceptionHandler())
                {
                    return await excHandler.HandleExceptionAndReturnHTTPStatusCode<RSVPController>(exc, _logger, requestId, this.HttpContext);
                }
            }
        }
    }
}
