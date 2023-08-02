using Microsoft.AspNetCore.Mvc;
using phone_shop_server.Business.APIResponse;
using phone_shop_server.Business.DTO.Brand;
using phone_shop_server.Business.DTO.Order;
using phone_shop_server.Business.DTO.Phone;
using phone_shop_server.Business.Service;

namespace phone_shop_server.API
{
    [ApiController]
    [Route("api/v{version:apiVersion}/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _orderService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _orderService.GetOneAsync(id));
        }
        [HttpGet("user")]
        public async Task<IActionResult> GetByUser()
        {
            return Ok(await _orderService.GetByUserIdAsync(HttpContext));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderCreateDto dto)
        {
            return Ok(await _orderService.CreateAsync(dto, HttpContext));
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] OrderPaymentUpdateDto dto)
        {
            return Ok(await _orderService.UpdatePaymentStatus(dto, HttpContext));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelOrder(string id)
        {
            return Ok(await _orderService.CancelOrder(HttpContext, id));
        }
    }
}
