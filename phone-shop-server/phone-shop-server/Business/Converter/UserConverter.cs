using phone_shop_server.Business.DTO.Address;
using phone_shop_server.Business.DTO.User;
using phone_shop_server.Database.Entity;
using phone_shop_server.Database.Repository;
using phone_shop_server.Util;

namespace phone_shop_server.Business.Converter
{
    public interface IUserConverter
    {
        Task<UserDto> UserToUserDto(AppUser user);
        Task<AppUser> ConvertUserUpdateToUser(UserUpdateDto dto);
        Task<IEnumerable<UserDto>> ConvertToListUserDto(IEnumerable<AppUser> users);
    }
    public class UserConverter : IUserConverter
    {
        private readonly IFileUtil _fileUtil;
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        public UserConverter(
            IFileUtil fileUtil,
            IUserRepository userRepository,
            IAddressRepository addressRepository
            )
        {
            _fileUtil = fileUtil;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
        }
        public async Task<UserDto> UserToUserDto(AppUser user)
        {
            var roles = await _userRepository.GetListRoleAsync(user.Id);
            return new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Avatar = user.Avatar,
                PhoneNumber = user.PhoneNumber,
                ResetPasswordToken = user.ResetPasswordToken,
                RefreshToken =  user.RefreshToken,
                IsBlock = user.IsBlock,
                Role = roles.ToList(),
                Address = (await _addressRepository.GetByUserIdAsync(user.Id)).ToList()
            };
    }
        public AppUser UserDtoToUser(UserDto dto)
        {
            return new AppUser();
        }
        public async Task<IEnumerable<UserDto>> ConvertToListUserDto(IEnumerable<AppUser> users)
        {
            List<UserDto> dtos = new List<UserDto>();
            foreach (var user in users)
            {
                dtos.Add(await UserToUserDto(user));
            }
            return dtos.ToList();
        }
        public async Task<AppUser> ConvertUserUpdateToUser(UserUpdateDto dto)
        {
            AppUser updateUser = await _userRepository.GetAsync(dto.Id);
            if (dto.Avatar != null)
            {
                string avatar = await _fileUtil.UploadAsync(dto.Avatar);
                updateUser.Avatar = avatar;
            }
            updateUser.Age = dto.Age;
            updateUser.Email = dto.Email;
            updateUser.LastName = dto.LastName;
            updateUser.FirstName = dto.FirstName;
            updateUser.PhoneNumber = dto.PhoneNumber;
            return updateUser;
        }
    }
}
