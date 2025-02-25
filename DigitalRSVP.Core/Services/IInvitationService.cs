using DigitalRSVP.Core.Models;

namespace DigitalRSVP.Core.Services
{
    public interface IInvitationService
    {
        public Task<Invitation> GetInvitationAsync(Guid id);
        public Task<IEnumerable<Invitation>> GetInvitationsByEventIdAsync(Guid eventId);

        public Task<bool> InvitationAuthorizedAsync(Guid inviteeId);
    }
}
