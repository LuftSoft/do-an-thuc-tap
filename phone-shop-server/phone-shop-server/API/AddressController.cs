using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using phone_shop_server.Business.APIResponse;
using phone_shop_server.Business.Converter;
using phone_shop_server.Business.DTO.Address;
using phone_shop_server.Business.DTO.Brand;
using phone_shop_server.Business.DTO.Phone;
using phone_shop_server.Business.Service;
using phone_shop_server.Database;
using phone_shop_server.Database.Entity;
using phone_shop_server.Database.Repository;
using phone_shop_server.Util;

namespace phone_shop_server.API
{
    [ApiController]
    [Route("api/v{version:apiVersion}/address")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IBrandService _brandService;
        private readonly IJwtUtil _jwtUtil;
        private readonly IUserService _userService;
        private readonly IAddressConverter _addressConverter;
        public AddressController(
            IBrandService brandService,
            IAddressRepository addressRepository,
            IJwtUtil jwtUtil,
            IUserService userService,
            AppDbContext appDbContext,
            IAddressConverter addressConverter
            )
        {
            _appDbContext = appDbContext;
            _addressConverter = addressConverter;
            _jwtUtil = jwtUtil;
            _userService = userService;
            _addressRepository = addressRepository;
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
        public async Task<APIResponse> Post(AddressDto dto)
        {
            try
            {
                var userId = await _userService.GetUserIdFromContext(HttpContext);
                if(userId == null)
                {
                    return new APIResponse()
                    {
                        code = Database.Enum.StatusCode.ERROR.ToString(),
                        message = "user is null"
                    };
                }
                var listAdd = await _addressRepository.GetByUserIdAsync(userId);
                if(listAdd == null || listAdd.Count() == 0)
                {
                    dto.IsDefault = true;
                }
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = await _addressRepository.CreateAsync(new Address() 
                    {
                        isDefault = (bool) dto.IsDefault,
                        DetailAddress = dto.DetailAddress,
                        HomeletId = dto.HomeletId,
                        Type = dto.Type,
                        UserId = userId.ToString(),
                    })
                };
            }
            catch (Exception ex)
            {
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.ERROR.ToString(),
                    message = "Failed!"
                };
            }
        }

        [HttpPut]
        public async Task<APIResponse> Put(AddressDto dto)
        {
            try
            {
                var userId = await _userService.GetUserIdFromContext(HttpContext);
                if (userId == null)
                {
                    return new APIResponse()
                    {
                        code = Database.Enum.StatusCode.ERROR.ToString(),
                        message = "user is null"
                    };
                }
                return new APIResponse()
                {
                    code = Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = await _addressRepository.UpdateAsync(new Address()
                    {
                        Id = Guid.Parse(dto.Id),
                        isDefault = (bool)dto.IsDefault,
                        DetailAddress = dto.DetailAddress,
                        HomeletId = dto.HomeletId,
                        Type = dto.Type,
                        UserId = userId,
                    })
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
        [HttpPatch("{id}")]
        public async Task<APIResponse> SetDault(string id) 
        {
            try
            {
                var address = await _appDbContext.Address.FirstOrDefaultAsync(a => a.Id.ToString() == id);
                if (address.isDefault) return new APIResponse()
                {
                    code = "SUCCESS"
                };
                var defaultAd = await _appDbContext.Address.Where(a => (a.UserId == address.UserId) && a.isDefault).FirstOrDefaultAsync();
                defaultAd.isDefault = false;
                address.isDefault = true;
                await _appDbContext.SaveChangesAsync();
                return new APIResponse()
                {
                    code = "SUCCESS"
                };
            }
            catch 
            {
                return new APIResponse()
                {
                    code = "ERROR"
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
                    data = await _addressRepository.DeleteAsync(id)
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
