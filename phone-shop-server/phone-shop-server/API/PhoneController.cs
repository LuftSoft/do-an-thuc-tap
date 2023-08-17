using Humanizer;
using Microsoft.AspNetCore.Mvc;
using phone_shop_server.Business.APIResponse;
using phone_shop_server.Business.DTO.Phone;
using phone_shop_server.Business.Service;
using phone_shop_server.Database.Enum;
using phone_shop_server.Database.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace phone_shop_server.API
{
    [ApiController]
    [Route("api/v{version:apiVersion}/phone")]
    public class PhoneController : ControllerBase
    {
        private readonly IPhoneService _phoneService;
        private readonly IPhoneRepository _phoneRepository;
        public PhoneController(
            IPhoneService phoneService,
            IPhoneRepository phoneRepository
            
        )
        {
            _phoneService = phoneService;
            _phoneRepository = phoneRepository;
        }

        [HttpGet]
        public async Task<APIResponse> Get(int? pageIndex, int? pageSize, [FromQuery] PhoneFilterDto? filterDto)
        {
            try
            {
                if (pageIndex == null) pageIndex = 0;
                if (pageSize == 0 || pageSize == null) pageSize = 10;
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = await _phoneService.GetAllAsync((int) pageIndex,(int) pageSize, filterDto)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.ERROR.ToString(),
                    message = ex.Message,
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
                    data = await _phoneService.GetOneAsync(id)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.ERROR.ToString(),
                    message = ex.Message,
                    data = null
                };
            }
        }

        [HttpGet("best-seller")]
        public async Task<APIResponse> BestSeller(int top)
        {
            try
            {
                
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = await _phoneRepository.GetBestSeller(top)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.ERROR.ToString(),
                    message = ex.Message,
                    data = null
                };
            }
        }
        [HttpGet("role")]
        public async Task<APIResponse> GetAllRole()
        {
            try
            {

                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = await _phoneRepository.GetAllRole()
                };
            }
            catch (Exception ex)
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.ERROR.ToString(),
                    message = ex.Message,
                    data = null
                };
            }
        }
        [HttpGet("dashboard")]
        public async Task<APIResponse> GetDashboard()
        {
            try
            {

                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = await _phoneRepository.AdminDashBoard()
                };
            }
            catch (Exception ex)
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.ERROR.ToString(),
                    message = ex.Message,
                    data = null
                };
            }
        }
        [HttpPost]
        public async Task<APIResponse> Post([FromForm] PhoneCreateDto dto)
        {
            try
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = await _phoneService.CreateAsync(dto)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.ERROR.ToString(),
                    message = ex.Message,
                    data = null
                };
            }
        }

        [HttpPut()]
        public async Task<APIResponse> Put([FromBody] PhoneUpdateDto dto)
        {
            try
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = await _phoneService.UpdateAsync(dto)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.ERROR.ToString(),
                    message = ex.Message,
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
                    data = await _phoneService.DeleteAsync(id)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.ERROR.ToString(),
                    message = ex.Message,
                    data = null
                };
            }
        }
    }
}
