namespace Event_Management.Requests{

    public class NewBooking{
        public Guid UserId{get; set; }
        public Guid EventId {get; set; }
        
    }
}