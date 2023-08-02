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
        public WarehouseController(
            IWarehouseTicketService warehouseTicketService
            ) 
        {
            _warehouseTicketService = warehouseTicketService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult Post([FromBody] WarehouseTicketCreateDto createDto)
        {
            return Ok();
        }
        [HttpPut]
        public IActionResult Put()
        {
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return Ok();
        }
    }
}
