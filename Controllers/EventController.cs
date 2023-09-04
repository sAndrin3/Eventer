using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Event_Management.Responses;
using Event_Management.Data;
using Event_Management.Entities;
using Event_Management.Requests;
using Event_Management.Services.Iservices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // Import the Authorization namespace

namespace Jitu_udemy.Controllers
{
    [Authorize] 
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public EventController(IMapper mapper, IEventService service)
        {
            _mapper = mapper;
            _eventService = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UserSuccess>> AddEvent(AddEvent newEvent)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == "Roles").Value;
            if(!string.IsNullOrWhiteSpace(role) && role == "admin")
            {
            try
            {
                var user = _mapper.Map<Event>(newEvent);
                var res = await _eventService.AddEventAsync(user);
                return CreatedAtAction(nameof(AddEvent), new UserSuccess(201, res));
            }
            catch (Exception ex)
            {
                return BadRequest(new UserSuccess(400, ex.Message));
            }
            
        }
        return BadRequest("You are not allowed");
        }

        [HttpGet("Location")]
        public async Task<ActionResult<IEnumerable<EventResponse>>> GetAllEvents(string? location)
        {
            var response = await _eventService.GetAllEventsAsync(location);
            var events = _mapper.Map<IEnumerable<EventResponse>>(response);
            return Ok(events);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventResponse>>> GetAllEvents()
        {
            var response = await _eventService.GetAllEventsAsync();
            var events = _mapper.Map<IEnumerable<EventResponse>>(response);
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventResponse>> GetEvent(Guid id)
        {
            var response = await _eventService.GetEventByIdAsync(id);
            if (response == null)
            {
                return NotFound(new UserSuccess(404, "Event Does Not Exist"));
            }
            var newevent = _mapper.Map<EventResponse>(response);
            return Ok(newevent);
        }

        [HttpPut("{id}")]
        [Authorize] 
        public async Task<ActionResult<UserSuccess>> UpdateEvent(Guid id, AddEvent UpdatedEvent)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == "Roles").Value;
            if (!string.IsNullOrWhiteSpace(role) && role == "admin")
            {
                try
                {
                    var response = await _eventService.GetEventByIdAsync(id);
                    if (response == null)
                    {
                        return NotFound(new UserSuccess(404, "Event Not Found"));
                    }

                    // Update
                    var updated = _mapper.Map(UpdatedEvent, response);
                    var res = await _eventService.UpdateEventAsync(updated);
                    return Ok(new UserSuccess(204, res));
                }
                catch (Exception ex)
                {
                    return BadRequest(new UserSuccess(400, ex.Message));
                }
            }
            else
            {
                
                return BadRequest("You are not allowed");
            }
        }


        [HttpDelete("{id}")]
        [Authorize] 
        public async Task<ActionResult<UserSuccess>> DeleteEvent(Guid id)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == "Roles").Value;
            if (!string.IsNullOrWhiteSpace(role) && role == "admin")
            {
                try
                {
                    var response = await _eventService.GetEventByIdAsync(id);
                    if (response == null)
                    {
                        return NotFound(new UserSuccess(404, "Event Does Not Exist"));
                    }

                    // Delete
                    var res = await _eventService.DeleteEventAsync(response);
                    return Ok(new UserSuccess(204, res));
                }
                catch (Exception ex)
                {
                    return BadRequest(new UserSuccess(400, ex.Message));
                }
            }
            else
            {
                
                return BadRequest("You are not allowed");
            }
        }

        [HttpGet("{eventId}/RegisteredUsers")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetEventRegisteredUsers(Guid eventId)
        {
            var response = await _eventService.GetEventByIdAsync(eventId);
            if (response == null)
            {
                return NotFound(new UserSuccess(404, "Event Does Not Exist"));
            }
            var usersregistered = await _eventService.GetEventRegisteredUsers(eventId);
            var users = _mapper.Map<IEnumerable<UserResponse>>(usersregistered);
            return Ok(users);
        }


        [HttpGet("{id}/AvailableSlots")]
        public async Task<ActionResult<int>> GetAvailableSlots(Guid id)
        {
            return await _eventService.GetAvailableSlotsAsync(id);

            // if (eventResponse == null)
            // {
            //     return NotFound(new UserSuccess(404, "Event Not Found"));
            // }

            // // Calculate available slots based on event's capacity and the number of registered users
            // // int availableSlots = eventResponse.Capacity - eventResponse.Users.Count; 

            // return Ok(eventResponse);
        }



    }

    public class CourseSuccess
    {
    }

    public class CoursesResponse
    {
    }
}