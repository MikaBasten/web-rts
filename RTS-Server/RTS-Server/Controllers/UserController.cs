using Core.IServices;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace RTS_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/users/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.PasswordHash))
                return BadRequest("Username and password are required.");

            var success = _userService.Register(user.Username, user.PasswordHash);

            if (!success)
                return Conflict("Username already exists.");

            return Ok("User registered successfully.");
        }

        // POST: api/users/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.PasswordHash))
                return BadRequest("Username and password are required.");

            var isValidUser = _userService.ValidateUser(user.Username, user.PasswordHash);

            if (!isValidUser)
                return Unauthorized("Invalid username or password.");

            // Here, generate JWT token and return it to the client (for simplicity, omitted)
            // string token = GenerateJwtToken(user.Username);
            // return Ok(new { Token = token });

            return Ok("User logged in successfully.");
        }
    }
}
