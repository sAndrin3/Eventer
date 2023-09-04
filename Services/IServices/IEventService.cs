using Event_Management.Entities;

namespace Event_Management.Services.Iservices{
    public interface IEventService{
        Task<string> AddEventAsync(Event newevent);
        Task<string> DeleteEventAsync(Event newevent);
        Task<string> UpdateEventAsync(Event newevent);
        Task<IEnumerable<Event>> GetAllEventsAsync(string location);
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(Guid id);
        Task<int> GetAvailableSlotsAsync(Guid eventId);
        Task<List<User>> GetEventRegisteredUsers(Guid id);
    }
}