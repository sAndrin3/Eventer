using Event_Management.Entities;

namespace Event_Management.Services.Iservices{
    public interface IUserService{
        // Add
        Task<string> AddUserAsync(User user);

            //Update
        Task<string> UpdateUserAsync(User user);

        //Delete
        Task<string> DeleteUserAsync(User user);

        //Get All USers
        Task<IEnumerable<User>> GetAllUsersAsync();

        // Gets single user
        Task <User> GetUserByIdAsync(Guid id);
        Task<bool> RegisterUserForEventAsync(UserRegistration registration);
         Task<IEnumerable<User>> GetUsersRegisteredForEventAsync(Guid eventId);

        
    }
}