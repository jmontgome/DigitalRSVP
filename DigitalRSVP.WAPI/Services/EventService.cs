using DigitalRSVP.Core.Models;
using DigitalRSVP.Core.Services;
using DigitalRSVP.WAPI.Repositories.Interfaces;

namespace DigitalRSVP.WAPI.Services
{
    public class EventService: IEventService
    {
        private readonly IEventRepository _eventRepo;

        public EventService(IEventRepository eventRepo)
        {
            this._eventRepo = eventRepo;
        }

        public async Task<Event> GetEventByIdAsync(Guid id)
        {
            return await this._eventRepo.GetEventByIdAsync(id);
        }
        public async Task<Event> GetEventByEmailAsync(string email)
        {
            return await this._eventRepo.GetEventByEmailAsync(email);
        }
    }
}
