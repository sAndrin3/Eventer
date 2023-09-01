using AutoMapper;
using Event_Management.Responses;
using Event_Management.Entities;
using Event_Management.Requests;


namespace Jitu_Udemy.Profiles{
    public class JituProfiles:Profile{
        public JituProfiles()
        {
            CreateMap<AddUser, User>().ReverseMap();
            CreateMap<UserResponse, User>().ReverseMap();

        
            CreateMap<AddEvent, Event>().ReverseMap();
            CreateMap<EventResponse, Event>().ReverseMap();

         
        }
    }
}