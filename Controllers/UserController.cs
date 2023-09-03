using AutoMapper;
using Event_Management.Services.Iservices;
using Event_Management.Entities;
using Event_Management.Requests;
using Event_Management.Responses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Jitu_Udemy.Controllers{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase{
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private Guid eventId;

        public UserController(IUserService service, IMapper mapper)
        {
            _mapper = mapper;
            _userService = service;
        }
        [HttpPost]
        public async Task<ActionResult<UserSuccess>> AddUser(AddUser newUser){
            var user = _mapper.Map<User>(newUser);
            var res = await _userService.AddUserAsync(user);
            return CreatedAtAction(nameof(AddUser),new UserSuccess(201, res));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers(){
            var response = await _userService.GetAllUsersAsync();
            var users = _mapper.Map<IEnumerable<UserResponse>>(response);
            return Ok(users);

        }

         [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUser(Guid id){
            var response = await _userService.GetUserByIdAsync(id);
            if(response == null){
                return NotFound(new UserSuccess(404, "User Does Not Exist"));
            }
            var user = _mapper.Map<UserResponse>(response);
            return Ok(user);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserSuccess>> UpdateUser(Guid id, AddUser UpdatedUser)
        {
            var response = await _userService.GetUserByIdAsync(id);
            if (response == null)
            {
                return NotFound(new UserSuccess(404, "User Does Not Exist"));
            }
            //update
            var updated = _mapper.Map(UpdatedUser, response);
            var res = await _userService.UpdateUserAsync(updated);
            return Ok(new UserSuccess(204, res));

        }

         [HttpDelete("{id}")]
        public async Task<ActionResult<UserSuccess>> DeleteUser(Guid id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            if (response == null)
            {
                return NotFound(new UserSuccess(404, "User Does Not Exist"));
            }
            //delete
           
            var res = await _userService.DeleteUserAsync(response);
            return Ok(new UserSuccess(204, res));

        }

        [HttpPost("RegisterForEvent/{eventId}")]
        public async Task<IActionResult> RegisterForEvent([FromBody] UserRegistrationRequest request)
        {
            try
            {
      
                var userRegistration = new UserRegistration
                {
                    EventId = eventId,
                    
                };

                // Save the user registration record to your database
                var registrationResult = await _userService.RegisterUserForEventAsync(userRegistration);

                // Check if the registration was successful
                if (registrationResult)
                {
                    return Ok(new UserSuccess(200, "User registered for the event successfully"));
                }
                else
                {
                    
                    return BadRequest(new UserSuccess(400, "User registration failed"));
                }
            }
            catch (Exception ex)
            {
               
            
                return StatusCode(500, new UserSuccess(500, ex.Message));
            }
        }

        [HttpGet("{eventId}/RegisteredUsers")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetEventRegisteredUsers(Guid eventId)
        {
            try
            {
                // Retrieve the list of users registered for the specified event
                var registeredUsers = await _userService.GetUsersRegisteredForEventAsync(eventId);

                // Check if there are any registered users
                if (registeredUsers == null || !registeredUsers.Any())
                {
                    return NotFound(new UserSuccess(404, "No users are registered for this event"));
                }

                // Map the registered users to UserResponse objects
                var userResponses = _mapper.Map<IEnumerable<UserResponse>>(registeredUsers);

                return Ok(userResponses);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new UserSuccess(500, ex.Message));
            }
        }




    }

    public class RegisterForEventRequest
    {
    }
}