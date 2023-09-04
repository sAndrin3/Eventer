using Event_Management.Entities;

namespace Event_Management.Responses{
    public class UserResponse{
        public Guid Id {get; set; }

        public string Name {get; set; } = string.Empty;
        public string Email {get; set; } = string.Empty;
        
        public int PhoneNumber {get; set;}

        // public List<Event> Events {get; set;}
    }
}