using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_api.Models;
using web_api.Services;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = WebRoles.Member)]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Authorize(Policy = WebRoles.Admin)]
        public async Task<ActionResult<List<User>>> GetAllUsers() => await _userService.GetAllUsers();
        


    }
}
