using Microsoft.AspNetCore.Mvc;

namespace phone_shop_server.API.User
{
    [ApiController]
    [Route("/api/v/user")]
    public class UserController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login()
        {
            return Ok("login success");
        }
    }
}
