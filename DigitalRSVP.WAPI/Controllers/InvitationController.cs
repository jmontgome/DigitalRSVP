using DigitalRSVP.Core.Models;
using DigitalRSVP.Core.Services;
using DigitalRSVP.WAPI.Handlers.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DigitalRSVP.WAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvitationController : Controller
    {
        private readonly ILogger<InvitationController> _logger;

        private readonly IInvitationService _invitationService;

        public InvitationController(ILogger<InvitationController> logger, IInvitationService invitationService)
        {
            this._logger = logger;
            this._invitationService = invitationService;
        }

        [HttpGet("id={id}")]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(Invitation))]
        public async Task<IActionResult> GetInvitation(string id)
        {
            Guid requestId = Guid.NewGuid();
            _logger.LogInformation($"[Request ID: {requestId}] Endpoint called @ {this.HttpContext.Request.Path} from {this.HttpContext.Connection.RemoteIpAddress}");
            try
            {
                if (Guid.TryParse(id, out Guid invitationId))
                {
                    Invitation inv = await _invitationService.GetInvitationAsync(invitationId);
                    if (inv != null)
                    {
                        return new JsonResult(inv);
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
                    return await excHandler.HandleExceptionAndReturnHTTPStatusCode<InvitationController>(exc, _logger, requestId, this.HttpContext);
                }
            }
        }
    }
}
