using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using phone_shop_server.Business.DTO.Warehouse;
using phone_shop_server.Business.Service;

namespace phone_shop_server.API
{
    [ApiController]
    [Route("api/v{version:apiVersion}/warehouse")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseTicketService _warehouseTicketService;
        private readonly IUserService _userService;
        public WarehouseController(
            IUserService userService,
            IWarehouseTicketService warehouseTicketService
            ) 
        {
            _userService = userService;
            _warehouseTicketService = warehouseTicketService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _warehouseTicketService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _warehouseTicketService.GetOneAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WarehouseTicketCreateDto createDto)
        {
            string userId = await _userService.GetUserIdFromContext(HttpContext);
            createDto.UserId = userId;
            createDto.Created = DateTime.Now;
            return Ok(await _warehouseTicketService.CreateAsync(createDto));
        }
        [HttpPut]
        public async Task<IActionResult> Put()
        {
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok();
        }
    }
}
