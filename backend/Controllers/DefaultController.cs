using GameStore.Models;
using GameStore.Models.Requests;
using GameStore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("api/")]
    public class DefaultController : Controller
    {
        private readonly ILogger<DefaultController> _logger;
        private readonly UserService _userService;

        public DefaultController(ILogger<DefaultController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }


        [HttpGet("access-unauthorized")]
        public IActionResult AccessUnauthorized()
        {
            return StatusCode(401, "Доступ закрыт");
        }


        [HttpGet("access-forbidden")]
        public IActionResult AccessDenied()
        {
            return StatusCode(403, "У вас недостаточно прав");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            string username = request.Username;
            string password = request.Password;
            var existingUser = await _userService.GetByUsername(username);

            if (
                existingUser != null &&
                username == existingUser.Username &&
                password == existingUser.Password
            )
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, existingUser.Role)
                };

                var identity = new ClaimsIdentity(claims, "Cookies");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("Cookies", principal);

                return Ok("Успешный вход");
            }

            return Unauthorized("Неверный логин или пароль");
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userService.Register(request);

            if (user == null)
            {
                return BadRequest(new { message = "Пользователь с таким именем уже существует." });
            }

            return Ok(user.ToUserDto());
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return Ok("Успешный выход");
        }
    }
}
