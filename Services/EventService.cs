using System.Runtime.Serialization;
using Event_Management.Data;
using Event_Management.Entities;
using Event_Management.responses;
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

        public async Task<IEnumerable<Event>> GetAllEventsAsync(string? location)
        {
            IQueryable<Event> query = _context.Events;

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(e => e.Location == location);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events.ToListAsync();
        }

       
        public async Task<Event> GetCourseByIdAsync(Guid id)
        {
            return await _context.Events.Where(x=>x.Id==id).FirstOrDefaultAsync();
        }

        public async Task<Event> GetEventByIdAsync(Guid id)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<string> UpdateEventAsync(Event newevent )
        {
            _context.Events.Update(newevent);
            await _context.SaveChangesAsync();
            return "Events updated successfully";
        }

          public async Task<int> GetAvailableSlotsAsync(Guid eventId)
        {
            var events = await _context.Events.Where(u=>u.Id == eventId)

             .Select(x=>new Numbers(){

                NumberOfUsers=x.Users.Count

             }).FirstOrDefaultAsync();

             var eventRes= await _context.Events.Where(u=>u.Id==eventId).FirstOrDefaultAsync();

             var totalBooking = events.NumberOfUsers;
             System.Console.WriteLine(totalBooking);
             System.Console.WriteLine(eventRes.Name);

            var availableslots = eventRes.Capacity - totalBooking;

            return availableslots;

            

        
        }
        
            public async Task<List<User>> GetEventRegisteredUsers(Guid id)
        {
            var result = await _context.Events.Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == id);
            return result.Users;
        }


        
    }

    [Serializable]
    internal class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string? message) : base(message)
        {
        }

        public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}