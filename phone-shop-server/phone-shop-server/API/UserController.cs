using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using phone_shop_server.Business.APIResponse;
using phone_shop_server.Business.DTO.User;
using phone_shop_server.Business.Service;
using phone_shop_server.Database;

namespace phone_shop_server.API
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _appDbContext;
        public UserController(
            IUserService userService,
            AppDbContext appDbContext
            ) 
        {
            _userService = userService; 
            _appDbContext = appDbContext;
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
        [HttpPut]
        public async Task<IActionResult> Put(UserUpdateDto update)
        {
            try
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == update.Id);
                user.PhoneNumber = update.PhoneNumber;
                user.FirstName = update.FirstName;
                user.LastName = update.LastName;
                user.Age = update.Age;
                _appDbContext.Users.Update(user);
                await _appDbContext.SaveChangesAsync();
                return Ok(true);
            }
            catch (Exception ex)
            {

                return Ok(false);
            }
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
