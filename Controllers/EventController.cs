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
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult<UserSuccess>> AddEvent(AddEvent newEvent)
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
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult<UserSuccess>> UpdateEvent(Guid id, AddEvent UpdatedEvent)
        {
            var response = await _eventService.GetEventByIdAsync(id);
            if (response == null)
            {
                return NotFound(new UserSuccess(404, "Event Not Found"));
            }
            //update
            var updated = _mapper.Map(UpdatedEvent, response);
            var res = await _eventService.UpdateEventAsync(updated);
            return Ok(new UserSuccess(204, res));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserSuccess>> DeleteEvent(Guid id)
        {
            var response = await _eventService.GetEventByIdAsync(id);
            if (response == null)
            {
                return NotFound(new UserSuccess(404, "Event Does Not Exist"));
            }
            //delete
            var res = await _eventService.DeleteEventAsync(response);
            return Ok(new UserSuccess(204, res));
        }

        [HttpGet("{id}/AvailableSlots")]
        public async Task<ActionResult<int>> GetAvailableSlots(Guid id)
        {
            var eventResponse = await _eventService.GetEventByIdAsync(id);

            if (eventResponse == null)
            {
                return NotFound(new UserSuccess(404, "Event Not Found"));
            }

            // Calculate available slots based on event's capacity and the number of registered users
            int availableSlots = eventResponse.Capacity - eventResponse.Users.Count; 

            return Ok(availableSlots);
        }



    }

    public class CourseSuccess
    {
    }

    public class CoursesResponse
    {
    }
}