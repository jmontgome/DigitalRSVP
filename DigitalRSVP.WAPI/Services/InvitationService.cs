using DigitalRSVP.Core.Models;
using DigitalRSVP.Core.Services;
using DigitalRSVP.WAPI.Repositories.Interfaces;

namespace DigitalRSVP.WAPI.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly IInvitationRepository _invRepo;

        public InvitationService(IInvitationRepository invRepo)
        {
            this._invRepo = invRepo;
        }

        public async Task<Invitation> GetInvitationAsync(Guid id)
        {
            return await _invRepo.GetInvitationAsync(id);
        }

        public async Task<bool> InvitationAuthorizedAsync(Guid inviteeId)
        {
            return await _invRepo.GetInvitationAsync(inviteeId) != null ? true : false;
        }
    }
}
