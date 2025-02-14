using GameStore.Models.Dto;
using GameStore.Models.Requests;
using GameStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;

        public UserController(ILogger<UserController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _userService.GetAll();
            return Ok(users.Select(user => user.ToUserDto()));
        }


        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] UserRequest request)
        {
            var result = await _userService.Update(request);

            return Ok(result);
        }


        [Authorize(Roles = "admin,manager")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.ToUserDto());
        }


        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            List<string> roles = ["admin", "manager"];

            return Ok(roles);
        }


        [Authorize(Roles = "admin,manager")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteById(id);

            return Ok(result);
        }


        /*[Authorize(Roles = "admin")]
        [HttpDelete("api/users")]
        public async Task<ActionResult> DeleteUsers()
        {
            var result = await _userService.DeleteAll();

            return Ok(result);
        }*/
    }
}
