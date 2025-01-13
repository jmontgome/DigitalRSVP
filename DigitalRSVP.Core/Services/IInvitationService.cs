using DigitalRSVP.Core.Models;

namespace DigitalRSVP.Core.Services
{
    public interface IInvitationService
    {
        public Task<Invitation> GetInvitation(Guid id);

        public Task<bool> InvitationAuthorized(Guid inviteeId);
    }
}
