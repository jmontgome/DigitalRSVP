using DigitalRSVP.Core.Models;

namespace DigitalRSVP.WAPI.Repositories.Interfaces
{
    public interface IInvitationRepository
    {
        public Task<Invitation> GetInvitation(Guid id);
        
        public Task<bool> InviteeAuthorized(Guid inviteeId);
    }
}
