using DigitalRSVP.Core.Models;
using DigitalRSVP.Core.Services;
using DigitalRSVP.WAPI.Repositories.Interfaces;

namespace DigitalRSVP.WAPI.Services
{
    public class RSVPService : IRSVPService
    {
        private readonly IRSVPRepository _rsvpRepo;
        private readonly IGuestRepository _guestRepo;

        public RSVPService(IRSVPRepository rsvpRepo, IGuestRepository guestRepo)
        {
            this._rsvpRepo = rsvpRepo;
            this._guestRepo = guestRepo;
        }
        
        public async Task<RSVP> GetRSVPAsync(Guid rsvpId)
        {
            RSVP ret = await _rsvpRepo.GetRSVPAsync(rsvpId);
            if (ret != null)
            {
                ret.Guests = await _guestRepo.GetGuestsByRSVPIdAsync(rsvpId);
            }
            return ret;
        }
        public async Task<RSVP> GetRSVPByInviteeAsync(Guid inviteeId)
        {
            RSVP ret = await _rsvpRepo.GetRSVPByInviteeAsync(inviteeId);
            if (ret != null)
            {
                ret.Guests = await _guestRepo.GetGuestsByRSVPIdAsync(ret.Id);
            }
            return ret;
        }

        public async Task SubmitRSVPAsync(RSVP rsvp)
        {
            await _rsvpRepo.SubmitRSVPAsync(rsvp);
            if (rsvp.Guests != null)
            {
                foreach (Guest guest in rsvp.Guests)
                {
                    await _guestRepo.UpsertGuest(guest);
                }
            }
        }
        public async Task EditRSVPAsync(RSVP rsvp)
        {
            await _rsvpRepo.EditRSVPAsync(rsvp);
            if (rsvp.Guests != null)
            {
                foreach (Guest guest in rsvp.Guests)
                {
                    await _guestRepo.UpsertGuest(guest);
                }
            }
        }
    }
}
