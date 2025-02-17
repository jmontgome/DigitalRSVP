using DigitalRSVP.Core.Models;

namespace DigitalRSVP.Core.Services
{
    public interface IEventService
    {
        public Task<Event> GetEventByIdAsync(Guid id);
    }
}
