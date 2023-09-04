using Event_Management.Data;
using Event_Management.Entities;
using Event_Management.Requests;
using Event_Management.Responses;
using Event_Management.Services.Iservices;
using Microsoft.EntityFrameworkCore;

namespace Event_Management.Services.Iservices {
    public class UserService : IUserService{
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context){
            _context = context;
        }
          public async Task<string> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return "User Created Successfully";
        }

        public async Task<string> DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return "User Deleted Successfully";
        }

        public async Task<IEnumerable<UserEventDto>> GetAllUsersAsync()
        {
            return await _context.Users.Select(u=>new UserEventDto(){
                Id=u.Id,
                Name=u.Name,
                Email=u.Email,
                Events= u.Events.Select(e=> new EventDto(){
                    Name=e.Name,
                    Description=e.Description,
                    TicketAmount = e.TicketAmount
                }).ToList()
            }).ToListAsync();
        }

       public async Task<User> GetUserByIdAsync(Guid id)
        {
          return await _context.Users.Where(x=>x.Id==id).FirstOrDefaultAsync();
         
        }

        public async Task<string> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return "User Updated Successfully";
        }

     public async Task<string> RegisterUserForEventAsync(NewBooking newBooking)
        {
            // Check if the user and event exist
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == newBooking.UserId);
            var events = await _context.Events.SingleOrDefaultAsync(x => x.Id == newBooking.EventId);

 

            if (user == null || events == null)
            {
                return "User or event not found";
            }

 

            // Check if the user is already registered for the event
            if (user.Events.Contains(events))
            {
                return "User is already registered for this event";
            }

 

            // Add the event to the user's list of registered events
            user.Events.Add(events);

 

            try
            {
                await _context.SaveChangesAsync();
                return "User registered for the event successfully";
            }
            catch (Exception ex)
            {
                return $"An error occurred while registering the user for the event: {ex.Message}";
            }
        }

        public async Task<IEnumerable<User>> GetUsersRegisteredForEventAsync(Guid eventId)
        {
            // Query  database to retrieve users registered for the specified event
            var registeredUsers = await _context.UserRegistrations
                .Where(ur => ur.EventId == eventId)
                .Select(ur => ur.User)
                .ToListAsync();

            return registeredUsers;
        }

           public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<string> RegisterUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return "User Registered";
        }

      



    }
}