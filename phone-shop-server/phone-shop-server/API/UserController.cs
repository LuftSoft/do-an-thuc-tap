using Microsoft.AspNetCore.Mvc;
using phone_shop_server.Business.APIResponse;
using phone_shop_server.Business.DTO.User;
using phone_shop_server.Business.Service;

namespace phone_shop_server.API
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(
            IUserService userService
            ) 
        {
            _userService = userService; 
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            return Ok(await _userService.GetAllUser());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(string id)
        {
            return Ok(await _userService.getUserDetailAsync(id));
        }
        [HttpGet("self-information")]
        public async Task<IActionResult> GetSelf()
        {
            return Ok(await _userService.getUserDetailAsync( await _userService.GetUserIdFromContext(HttpContext)));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            return Ok(await _userService.LoginService(dto));
        }
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromForm] UserSignUpDto dto)
        {
            APIResponse response = await _userService.SignupService(dto);
            return Ok(response);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(FogotPasswordDto dto)
        {
            return Ok(await _userService.fogotPasswordService(dto));
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            return Ok(await _userService.resetPasswordService(dto));
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            return Ok(await _userService.changePasswordService(dto, HttpContext));
        }
    }
}
