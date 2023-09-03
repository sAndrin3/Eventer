namespace Event_Management.Requests
{
    public class UserRegistrationRequest
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
    }
}