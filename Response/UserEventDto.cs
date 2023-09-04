using Event_Management.Entities;

namespace Event_Management.Responses{
    public class UserEventDto{
        public Guid Id {get; set ;}
        public string Name {get; set; } = string.Empty;
        public string Email {get; set; } =string.Empty;
        public List<EventDto> Events {get; set; } = new List<EventDto>();

    }
    public class EventDto{
        public string Name {get; set;} =string.Empty;
        public string Description {get; set; } =string.Empty;
        public int TicketAmount {get; set; }

    }
}