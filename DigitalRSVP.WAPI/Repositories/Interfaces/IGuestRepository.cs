using DigitalRSVP.Core.Models;

namespace DigitalRSVP.WAPI.Repositories.Interfaces
{
    public interface IGuestRepository
    {
        public Task UpsertGuest(Guest guest);
        public Task<IEnumerable<Guest>> GetGuestsByRSVPIdAsync(Guid id);
    }
}
