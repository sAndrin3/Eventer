using AutoMapper;
using Event_Management.Services.Iservices;
using Event_Management.Entities;
using Event_Management.Requests;
using Event_Management.Responses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Event_Management.Request;


namespace Jitu_Udemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private Guid eventId;
        private IConfiguration? configuration;

        public UserController(IUserService service, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _userService = service;
            _config = configuration;
        }
        [HttpPost("adduser")]
        public async Task<ActionResult<UserSuccess>> AddUser(AddUser newUser)
        {
            var user = _mapper.Map<User>(newUser);
            var res = await _userService.AddUserAsync(user);
            return CreatedAtAction(nameof(AddUser), new UserSuccess(201, res));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserEventDto>>> GetAllUsers()
        {
            var response = await _userService.GetAllUsersAsync();
            // var users = _mapper.Map<IEnumerable<UserResponse>>(response);
            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUser(Guid id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            if (response == null)
            {
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
        public async Task<ActionResult<string>> RegisterForEvent( NewBooking newBooking)
        {
            try
            {


                // Save the user registration record to your database
                var registrationResult = await _userService.RegisterUserForEventAsync(newBooking);

                // Check if the registration was successful
                if (registrationResult != null) 
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

    


        [HttpPost("registerUser")]

        public async Task<ActionResult<string>> registerUser(AddUser addUser)
        {
            var newUser = _mapper.Map<User>(addUser);
            //Hash password
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            //newUser.Role = "Admin";
            var res = await _userService.RegisterUser(newUser);
            return CreatedAtAction(nameof(registerUser), res);
        }

        [HttpPost("login")]

        public async Task<ActionResult<string>> registerUser(LoginUser logUser)
        {
            //check if user with that email exists

            var existingUser = await _userService.GetUserByEmail(logUser.Email);
            if (existingUser == null)
            {
                return NotFound("Invalid Credential");
            }
            // users exists

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(logUser.Password, existingUser.Password);
            if (!isPasswordValid)
            {
                return NotFound("Invalid Credential");
            }

            //i provided the right credentials

            //create Token
            var token = CreateToken(existingUser);

            return Ok(token);
        }


        private string CreateToken(User user)
        {
            //key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("TokenSecurity:SecretKey")));
            //Signing Credentials
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //payload-data

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Names", user.Name));
            claims.Add(new Claim("Sub", user.Id.ToString()));
            claims.Add(new Claim("Roles", user.Roles));

            //create Token 
            var tokenGenerated = new JwtSecurityToken(
                _config["TokenSecurity:Issuer"],
                _config["TokenSecurity:Audience"],
                signingCredentials: cred,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenGenerated);
            return token;
        }



    }

    public class RegisterForEventRequest
    {
    }
}