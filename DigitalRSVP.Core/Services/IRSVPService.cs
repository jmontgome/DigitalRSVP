using DigitalRSVP.Core.Models;

namespace DigitalRSVP.Core.Services
{
    public interface IRSVPService
    {
        public Task<RSVP> GetRSVP(Guid rsvpId);
        public Task<RSVP> GetRSVPByInvitee(Guid inviteeId);
        
        public Task SubmitRSVP(RSVP rsvp);
        public Task EditRSVP(RSVP rsvp);
    }
}
