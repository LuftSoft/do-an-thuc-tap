using Microsoft.AspNetCore.Mvc;
using phone_shop_server.Business.APIResponse;
using phone_shop_server.Business.DTO.Brand;
using phone_shop_server.Business.DTO.Phone;
using phone_shop_server.Business.DTO.Supplier;
using phone_shop_server.Business.Service;

namespace phone_shop_server.API
{
    [ApiController]
    [Route("api/v{version:apiVersion}/supplier")]
    public class SupplierController: ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _supplierService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _supplierService.GetOneAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SupplierCreateDto dto)
        {
            return Ok(await _supplierService.CreateAsync(dto));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SupplierDto dto)
        {
            return Ok(await _supplierService.UpdateAsync(dto));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _supplierService.DeleteAsync(id));
        }
    }
}
