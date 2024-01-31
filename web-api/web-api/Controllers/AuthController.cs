using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using web_api.Models;
using web_api.Services;

namespace web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;

        public AuthController(AuthService authService, UserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            User? user = await _userService.GetUserByUserName(request.Username);

            if (user != null)
                return BadRequest("User already registered");

            user = new User()
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                Role = WebRoles.Member
            };

            await _userService.CreateUser(user);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            User? user = await _userService.GetUserByUserName(request.Username);

            if (user == null)
                return BadRequest("Username not found");

            if (!request.Password.Equals(user.Password))
                return BadRequest("Invalid password");

            string token = _authService.CreateToken(user);

            Response.Cookies.Append("Token", token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                IsEssential = true,
                Expires = DateTime.Now.AddDays(1)
            });

            return Ok(user.Username);
        }

        [HttpPost("logout")]
        public ActionResult Logout()
        {
            if (Request.Cookies["Token"] == null)
                return BadRequest("Not logged in");

            Response.Cookies.Append("Token", "", new CookieOptions 
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                IsEssential = true,
                Expires = DateTime.Now.AddDays(-1)
            });

            return Ok();
        }

        [HttpGet("checkloggedin")]
        [Authorize(Policy = WebRoles.Member)]
        public ActionResult CheckLoggedIn()
        {
            string? username = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            if (username == null)
                return BadRequest("Username not found in jwt claim");

            return Ok(username);
        }

        [HttpGet("role")]
        public ActionResult<string> GetMyRole()
        {
            string role = HttpContext.User.FindFirstValue(ClaimTypes.Role);
            if (String.IsNullOrWhiteSpace(role))
                return BadRequest("Not logged in");

            return Ok(role);
        }
    }
}