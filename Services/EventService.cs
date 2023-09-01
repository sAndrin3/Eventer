using Event_Management.Data;
using Event_Management.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event_Management.Services.Iservices{
    public class EventService : IEventService{
        private readonly ApplicationDbContext _context;
        public EventService(ApplicationDbContext context){
            _context = context;
        }
              public async Task<string> AddEventAsync(Event newevent)
        {
            _context.Events.Add(newevent);
            await _context.SaveChangesAsync();
            return "Event added successfully";
        }

        public async Task<string> DeleteEventAsync(Event newevent)
        {
            _context.Events.Remove(newevent);
            await _context.SaveChangesAsync();
            return "Event deleted successfully";
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync(string location)
        {
            return await _context.Events.ToListAsync();
        }

       
        public async Task<Event> GetCourseByIdAsync(Guid id)
        {
            return await _context.Events.Where(x=>x.Id==id).FirstOrDefaultAsync();
        }

        public Task<Event> GetEventByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdateEventAsync(Event newevent )
        {
            _context.Events.Update(newevent);
            await _context.SaveChangesAsync();
            return "Events updated successfully";
        }

        
    }
}