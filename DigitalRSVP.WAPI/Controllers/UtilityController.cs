﻿using DigitalRSVP.Core.Models;
using DigitalRSVP.Core.Services;
using DigitalRSVP.WAPI.Handlers.Exceptions;
using DigitalRSVP.WAPI.Reporting;
using DigitalRSVP.WAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Net.Mail;
using System.Net.Mime;

namespace DigitalRSVP.WAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtilityController : Controller
    {
        private readonly ILogger<UtilityController> _logger;
        private readonly IEmailService _emailService;
        private readonly IRSVPService _rsvpService;
        private readonly IEventService _eventService;
        private readonly IInvitationService _inviteService;

        public UtilityController(ILogger<UtilityController> logger,
            IRSVPService rsvpService, IEventService eventService,
            IInvitationService inviteService,
            IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
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
        [EnableRateLimiting(ApplicationConstants.STRICT_RATE_LIMITER_POLICY_NAME)]
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
                        string emailBody = reportFactory.GenerateEmailReportBody();
                        try
                        {
                            using (MemoryStream csvBuffer = reportFactory.GenerateCSVFile())
                            {
                                this._emailService.SendEmail($"RSVP Report for {eventInServer.Name}", emailBody, email, new List<Attachment> { new Attachment(csvBuffer, $"Report_{eventInServer.Id}.csv") });
                            }
                        }
                        catch (Exception exc)
                        {
                            this._emailService.SendEmail($"RSVP Report for {eventInServer.Name}", emailBody, email, null);
                            _logger.LogError($"[Request ID: {requestId}] Message was sent but an exception occured preventing the report file from being created. {exc}. \r\n\tClient: {this.HttpContext.Request.Path} called by {this.HttpContext.Connection.RemoteIpAddress}.");
                        }
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
