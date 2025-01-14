using DigitalRSVP.Core.Models;

namespace DigitalRSVP.Core.Services
{
    public interface IInvitationService
    {
        public Task<Invitation> GetInvitationAsync(Guid id);

        public Task<bool> InvitationAuthorizedAsync(Guid inviteeId);
    }
}
