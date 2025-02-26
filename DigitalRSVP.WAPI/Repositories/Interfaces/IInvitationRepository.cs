using DigitalRSVP.Core.Models;

namespace DigitalRSVP.WAPI.Repositories.Interfaces
{
    public interface IInvitationRepository
    {
        public Task<Invitation> GetInvitationAsync(Guid id);
        public Task<IEnumerable<Invitation>> GetInvitationByEventIdAsync(Guid eventId);
    }
}
