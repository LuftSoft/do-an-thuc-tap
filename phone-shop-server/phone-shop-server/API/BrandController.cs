using Microsoft.AspNetCore.Mvc;
using phone_shop_server.Business.APIResponse;
using phone_shop_server.Business.DTO.Brand;
using phone_shop_server.Business.DTO.Phone;
using phone_shop_server.Business.Service;

namespace phone_shop_server.API
{
    [ApiController]
    [Route("api/v{version:apiVersion}/brand")]
    public class BrandController
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<APIResponse> Get()
        {
            //return await _brandService.GetAllAsync();
            var data = await _brandService.GetAllAsync();
            Console.WriteLine(data);
            try
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = data
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.ERROR.ToString(),
                    data = null
                };
            }
        }

        [HttpGet("{id}")]
        public async Task<APIResponse> Get(string id)
        {
            try
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = await _brandService.GetOneAsync(id)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.ERROR.ToString(),
                    data = null
                };
            }
        }

        [HttpPost]
        public async Task<APIResponse> Post([FromForm] BrandCreateDto dto)
        {
            try
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = await _brandService.CreateAsync(dto)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.ERROR.ToString(),
                    data = null
                };
            }
        }

        [HttpPut("{id}")]
        public async Task<APIResponse> Put([FromBody] BrandDto dto)
        {
            try
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = await _brandService.UpdateAsync(dto)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.ERROR.ToString(),
                    data = null
                };
            }
        }
        [HttpDelete("{id}")]
        public async Task<APIResponse> Delete(string id)
        {
            try
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = await _brandService.DeleteAsync(id)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.ERROR.ToString(),
                    data = null
                };
            }
        }
    }
}
