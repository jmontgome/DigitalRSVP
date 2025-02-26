using DigitalRSVP.Core.Models;

namespace DigitalRSVP.WAPI.Repositories.Interfaces
{
    public interface IRSVPRepository
    {
        public Task<RSVP> GetRSVPAsync(Guid rsvpId);
        public Task<RSVP> GetRSVPByInviteeAsync(Guid inviteeId);
        public Task<IEnumerable<RSVP>> GetRSVPsByEventIdAsync(Guid eventId);

        public Task SubmitRSVPAsync(RSVP rsvp);
        public Task EditRSVPAsync(RSVP rsvp);
    }
}
