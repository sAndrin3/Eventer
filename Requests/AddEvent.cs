namespace Event_Management.Requests{
    public class AddEvent{
      
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int TicketAmount { get; set; }
        
    }
}