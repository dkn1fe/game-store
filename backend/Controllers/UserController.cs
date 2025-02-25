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
        public async Task<ActionResult> GetUsers()
        {
            var result = await _userService.GetAll();
            return Ok(result);
        }


        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromForm] UserRequest request)
        {
            var result = await _userService.Update(request);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }


        [Authorize(Roles = "admin,manager")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var result = await _userService.GetById(id);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
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

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
