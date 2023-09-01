using Event_Management.Data;
using Event_Management.Entities;
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

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
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

}
}