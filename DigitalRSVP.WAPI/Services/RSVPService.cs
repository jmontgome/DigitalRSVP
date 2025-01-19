using DigitalRSVP.Core.Models;
using DigitalRSVP.Core.Services;
using DigitalRSVP.WAPI.Repositories.Interfaces;

namespace DigitalRSVP.WAPI.Services
{
    public class RSVPService : IRSVPService
    {
        private readonly IRSVPRepository _rsvpRepo;

        public RSVPService(IRSVPRepository rsvpRepo)
        {
            this._rsvpRepo = rsvpRepo;
        }
        
        public async Task<RSVP> GetRSVPAsync(Guid rsvpId)
        {
            return await _rsvpRepo.GetRSVPAsync(rsvpId);
        }
        public async Task<RSVP> GetRSVPByInviteeAsync(Guid inviteeId)
        {
            return await _rsvpRepo.GetRSVPByInviteeAsync(inviteeId);
        }

        public async Task SubmitRSVPAsync(RSVP rsvp)
        {
            await _rsvpRepo.SubmitRSVPAsync(rsvp);
        }
        public async Task EditRSVPAsync(RSVP rsvp)
        {
            await _rsvpRepo.EditRSVPAsync(rsvp);
        }
    }
}
