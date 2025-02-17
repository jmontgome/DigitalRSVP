using DigitalRSVP.Core.Models;

namespace DigitalRSVP.WAPI.Repositories.Interfaces
{
    public interface IEventRepository
    {
        public Task<Event> GetEventByIdAsync(Guid id);
    }
}
