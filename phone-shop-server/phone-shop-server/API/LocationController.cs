using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using phone_shop_server.Business.APIResponse;
using phone_shop_server.Database.Enum;
using phone_shop_server.Database.Repository;

namespace phone_shop_server.API
{
    [ApiController]
    [Route("api/v{version:apiVersion}/location")]
    public class LocationController : ControllerBase
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly IHomeletRepository _homeletRepository;
        public LocationController
            (
            IProvinceRepository provinceRepository,
            IDistrictRepository districtRepository, 
            IHomeletRepository homeletRepository
            ) 
        {
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _homeletRepository = homeletRepository;
        }
        // GET: LocationController
        [HttpGet("province")]
        public async Task<IActionResult> getAllProvince()
        {
            try
            {
                var result = await _provinceRepository.GetListProvine();
                return Ok(new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = result
                });
            }
            catch(Exception ex)
            {
                return Ok(new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.ERROR.ToString(),
                    message = ex.Message
                });
            }
        }

        [HttpGet("district")]
        public async Task<IActionResult> getDistrictByProvince(string id)
        {
            try
            {
                var result = await _districtRepository.GetByProvineIdAsync(id);
                return Ok(new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.ERROR.ToString(),
                    message = ex.Message
                });
            }
        }

        [HttpGet("homelet")]
        public async Task<IActionResult> getHomeletByDistrict(string id)
        {
            try
            {
                var result = await _homeletRepository.GetByDistrictIdAsync(id);
                return Ok(new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.ERROR.ToString(),
                    message = ex.Message
                });
            }
        }

        [HttpGet("province/filter")]
        public async Task<IActionResult> getProvinceByName(string name)
        {
            try
            {
                var result = await _provinceRepository.FindByNameAsync(name);
                return Ok(new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.ERROR.ToString(),
                    message = ex.Message
                });
            }
        }

        [HttpGet("district/filter")]
        public async Task<IActionResult> getDistrictByName(string name)
        {
            try
            {
                var result = await _districtRepository.GetByNameAsync(name);
                return Ok(new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.ERROR.ToString(),
                    message = ex.Message
                });
            }
        }

        [HttpGet("homelet/filter")]
        public async Task<IActionResult> getHomeletByName(string name)
        {
            try
            {
                var result = await _homeletRepository.GetByNameAsync(name);
                return Ok(new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.ERROR.ToString(),
                    message = ex.Message
                });
            }
        }
    }
}
