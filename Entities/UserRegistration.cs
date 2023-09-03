
using Microsoft.EntityFrameworkCore;

namespace Event_Management.Entities 
{
    [Keyless]
    public class UserRegistration
    {
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public User User { get; set; }
    }
}
