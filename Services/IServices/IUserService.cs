using Event_Management.Entities;
using Event_Management.Requests;
using Event_Management.Responses;

namespace Event_Management.Services.Iservices{
    public interface IUserService{
        // Add
        Task<string> AddUserAsync(User user);

            //Update
        Task<string> UpdateUserAsync(User user);

        //Delete
        Task<string> DeleteUserAsync(User user);

        //Get All USers
        Task<IEnumerable<UserEventDto>> GetAllUsersAsync();

        // Gets single user
        Task <User> GetUserByIdAsync(Guid id);
        Task<string> RegisterUserForEventAsync(NewBooking newBooking);
         Task<IEnumerable<User>> GetUsersRegisteredForEventAsync(Guid eventId);
         Task<string> RegisterUser(User user);

        Task<User> GetUserByEmail(string email);   
         

        
    }
}