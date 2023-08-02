using Microsoft.AspNetCore.Identity;
using phone_shop_server.Business.Converter;
using phone_shop_server.Business.DTO.Address;
using phone_shop_server.Business.DTO.User;
using phone_shop_server.Database.Entity;
using phone_shop_server.Database.Enum;
using phone_shop_server.Database.Repository;
using phone_shop_server.Util;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace phone_shop_server.Business.Service
{
    public class UserService : IUserService
    {
        private readonly IJwtUtil _jwtUtil;
        private readonly IMailUtil _mailUtil;
        private readonly IFileUtil _fileUtil;
        private readonly IConfiguration _configuration;
        private readonly IUserConverter _userConverter;
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        public UserService(
            IJwtUtil jwtUtil,
            IMailUtil sendMailUtil,
            IFileUtil uploadFileUtil,
            IConfiguration configuration,
            IUserConverter userConverter,
            IUserRepository userRepository,
            IAddressRepository addressRepository
            )
        {
            _jwtUtil = jwtUtil;
            _mailUtil = sendMailUtil;
            _fileUtil = uploadFileUtil;
            _configuration = configuration;
            _userConverter = userConverter;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
        }
        public async Task<string?> GetUserIdFromToken(string token)
        {
            string userName = _jwtUtil.getUserNameFromToken(token);
            if (userName == null)
            {
                return null;
            }
            return (await _userRepository.GetAsync(userName)).Id;

        }
        public async Task<string?> GetUserIdFromContext(HttpContext context)
        {
            string userId = _jwtUtil.getUserIdFromToken(context.Request.Headers.Authorization.FirstOrDefault().Split(" ")[1]);
            if (userId == null)
            {
                return null;
            }
            return (await _userRepository.GetAsync(userId)).Id;
        }
        public async Task<string> GetTokenFromContext(HttpContext context)
        {
            return context.Request.Headers.Authorization.FirstOrDefault();
        }
        public async Task<UserDto?> getUserDetailAsync(string userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user != null)
            {
                return await _userConverter.UserToUserDto(user);
            }
            return null;
        }
        public async Task<bool> DeleteAsync(string delUserId)
        {
            return await _userRepository.DeleteAsync(delUserId);
        }
        public async Task<APIResponse.APIResponse> UpdateAsync(UserUpdateDto dto)
        {
            try
            {
                //string userId = await this.GetTokenFromContext();
                AppUser user = await _userRepository.GetAsync(dto.Id);
                if (await _userRepository.IsEmailExist(dto.Email))
                    return new APIResponse.APIResponse()
                    {
                        code = StatusCode.ERROR.ToString(),
                        message = "Email already exist!"
                    };
                AppUser updateUser = await _userConverter.ConvertUserUpdateToUser(dto);
                var isSuccess = await _userRepository.UpdateAsync(updateUser);
                if (isSuccess != null)
                    return new APIResponse.APIResponse()
                    {
                        code = StatusCode.SUCCESS.ToString(),
                        data = updateUser
                    };
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = "Update uuser failed!"
                };
            }
            catch (Exception ex)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = "Error occur when update user"
                };
            }
        }
        public async Task<APIResponse.APIResponse> LoginService(UserLoginDto dto)
        {
            AppUser user = await _userRepository.GetByEmailAsync(dto.Email);
            if(user == null)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = "Email is not exists in system!"
                };
            }
            var result = await _userRepository.CheckPassWord(user.Id, dto.Password);
            if (!result)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = "Email or password is incorrect!"
                };
            }
            var userRoles = await _userRepository.GetListRoleAsync(user.Id);
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, dto.Email));
            claims.Add(new Claim(ClaimTypes.Actor, user.Id));
            foreach(var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(_jwtUtil.GenerateAccessToken(claims, user.Id));
            var refreshToken = new JwtSecurityTokenHandler().WriteToken(_jwtUtil.GenerateRefreshToken(claims, user.Id));
            user.RefreshToken = refreshToken;
            await _userRepository.UpdateAsync(user);
            return new APIResponse.APIResponse()
            {
                code = StatusCode.SUCCESS.ToString(),
                data = new
                {
                    accessToken = accessToken,
                    refreshToken = refreshToken
                }
            };
        }
        public async Task<APIResponse.APIResponse> GetAllUser()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                List<UserDto> dtos = (await _userConverter.ConvertToListUserDto(users)).ToList();
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString(),
                    data = dtos
                };
            }
            catch(Exception ex)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = ex.Message
                };
            }
        }
        public async Task<APIResponse.APIResponse> AddRoleToUser(string userId, string role)
        {
            AppUser user = await _userRepository.GetAsync(userId);
            var roles = await _userRepository.GetListRoleAsync(userId);
            if (roles.Contains(role))
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = "Role already exist!"
                };
            }
            var success = await _userRepository.addUserRoleAsync(user, role);
            if (!success)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = "Add Role failed!"
                };
            }
            return new APIResponse.APIResponse
            {
                code = StatusCode.SUCCESS.ToString(),
                data = _userConverter.UserToUserDto(user)
            };
        }
        public async Task<APIResponse.APIResponse> RemoveRoleFromUser(string userId, string role)
        {
            var roles = await _userRepository.GetListRoleAsync(userId);
            if (!roles.Contains(role))
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = "Role not exist in user!"
                };
            }
            var user = await _userRepository.GetAsync(userId);
            var success = await _userRepository.removeUserRoleAsync(user, role);
            if (!success)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = "Remove Role failed!"
                };
            }
            return new APIResponse.APIResponse
            {
                code = StatusCode.SUCCESS.ToString(),
                data = _userConverter.UserToUserDto(user)
            };
        }
        public async Task<APIResponse.APIResponse> SignupService(UserSignUpDto dto)
        {
            if (!dto.Role.Contains(DbUserRole.ADMIN.ToString()) && !dto.Role.Contains(DbUserRole.CUSTOMER.ToString())
                    && !dto.Role.Contains(DbUserRole.STAFF.ToString()))
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = "User must have role!"
                };
            }
            var userExist = await _userRepository.GetByEmailAsync(dto.Email);
            if (userExist == null)
            {
                AppUser user = new AppUser()
                {
                    Email = dto.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = dto.Email,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                };
                if (dto.Avatar != null)
                {
                    user.Avatar = await _fileUtil.UploadAsync(dto.Avatar);
                }
                await _userRepository.AddNewPassword(user, dto.Password);
                var result = await _userRepository.CreateAsync(user);
                //role user is role default
                switch (dto.Role)
                {
                    case "ADMIN":
                        await _userRepository.addUserRoleAsync(user, DbUserRole.ADMIN.ToString());
                        break;
                    case "STAFF":
                        await _userRepository.addUserRoleAsync(user, DbUserRole.STAFF.ToString());
                        break;
                    case "CUSTOMER":
                        await _userRepository.addUserRoleAsync(user, DbUserRole.CUSTOMER.ToString());
                        break;
                }
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString(),
                    data = await _userConverter.UserToUserDto(user)
                };
            }
            return new APIResponse.APIResponse()
            {
                code = StatusCode.ERROR.ToString(),
                message = "Email already exist!"
            };
        }
        public async Task<APIResponse.APIResponse> fogotPasswordService(FogotPasswordDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = "Email is not exists!"
                };
            //else if (!_jwtUtil.isTokenExpired(user.ResetPasswordToken))
            //{
            //    return new APIResponse.APIResponse()
            //    {
            //        code = StatusCode.ERROR.ToString(),
            //        message = "Token is invalid!"
            //    };
            //}
            var token = new JwtSecurityTokenHandler().WriteToken(_jwtUtil.GenerateResetPasswordApiToken(user.Id));
            var result = await _mailUtil.SendMailAsync(user.Email, "Please click to the link bellow to reset your password",
                $"{dto.Url}/{token}");
            //user.ResetPasswordToken = token;
            //if (result.Success == true) await _userRepository.UpdateAsync(user);
            return new APIResponse.APIResponse()
            {
                code = StatusCode.SUCCESS.ToString(),
                data = result
            };
        }
        public async Task<APIResponse.APIResponse> resetPasswordService(ResetPasswordDto dto)
        {
            string userName = _jwtUtil.getUserNameFromToken(dto.Token);
            bool isSuccess = await _userRepository.ResetPassword(userName, dto.NewPassword);
            if (isSuccess)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString()
                };
            }
            return new APIResponse.APIResponse()
            {
                code = StatusCode.ERROR.ToString(),
                message = "Reset password failed"
            };
        }
        public async Task<APIResponse.APIResponse> changePasswordService(ChangePasswordDto dto, HttpContext context)
        {
            string userId = await this.GetUserIdFromContext(context);
            bool isSuccess = await _userRepository.ChangePassword(userId, dto.OldPassword, dto.NewPassword);
            if (isSuccess)
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.SUCCESS.ToString()
                };
            return new APIResponse.APIResponse()
            {
                code = StatusCode.ERROR.ToString(),
                message = "Change password failed!"
            };
        }
        //public async Task<IBlockAndUnlockUserInteractor.Response> BlockUserAsync(IBlockAndUnlockUserInteractor.Request request)
        //{
        //    string userId = await GetUserIdFromToken(request.token);
        //    List<string> userRoles = (await _userRepository.GetListRoleOfUser(userId)).ToList();
        //    List<string> blockUuserRoles = (await _userRepository.GetListRoleOfUser(request.userId)).ToList();
        //    if (userRoles.Count == 0 || blockUuserRoles.Count == 0
        //        || !userRoles.Contains(DbUserRole.Owner) || blockUuserRoles.Contains(DbUserRole.Owner))
        //    {
        //        return new IBlockAndUnlockUserInteractor.Response()
        //        {
        //            Success = false,
        //            Message = "User don't have permission!"
        //        };
        //    }
        //    await _userRepository.BlockAsync(request.userId);
        //    return new IBlockAndUnlockUserInteractor.Response()
        //    {
        //        Success = true,
        //        Message = "Block user success"
        //    };
        //}
        //public async Task<IBlockAndUnlockUserInteractor.Response> UnlockUserAsync(IBlockAndUnlockUserInteractor.Request request)
        //{
        //    string userId = await GetUserIdFromToken(request.token);
        //    List<string> userRoles = (await _userRepository.GetListRoleOfUser(userId)).ToList();
        //    List<string> blockUuserRoles = (await _userRepository.GetListRoleOfUser(request.userId)).ToList();
        //    if (userRoles.Count == 0 || blockUuserRoles.Count == 0
        //        || !userRoles.Contains("owner"))
        //    {
        //        return new IBlockAndUnlockUserInteractor.Response()
        //        {
        //            Success = false,
        //            Message = "User don't have permission!"
        //        };
        //    }
        //    await _userRepository.UnlockAsync(request.userId);
        //    return new IBlockAndUnlockUserInteractor.Response()
        //    {
        //        Success = true,
        //        Message = "Unlock user success"
        //    };
        //}
        public async Task<APIResponse.APIResponse> RefreshToken(string token, HttpContext context)
        {
            string userId = await this.GetUserIdFromContext(context);
            AppUser user = await _userRepository.GetAsync(userId);
            if (user.RefreshToken != null && user.RefreshToken != token)
            {
                return new APIResponse.APIResponse()
                {
                    code = StatusCode.ERROR.ToString(),
                    message = "Token is invalid!"
                };
            }
            var liRoles = await _userRepository.GetListRoleAsync(user.Id);
            List<Claim> userRoles = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Actor, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var role in liRoles)
            {
                userRoles.Add(new Claim(ClaimTypes.Role, role));
            }
            string accessToken = new JwtSecurityTokenHandler().WriteToken(_jwtUtil.GenerateAccessToken(userRoles, user.Id));
            string refreshToken = new JwtSecurityTokenHandler().WriteToken(_jwtUtil.GenerateRefreshToken(userRoles, user.Id));
            user.RefreshToken = refreshToken;
            await _userRepository.UpdateAsync(user);
            return new APIResponse.APIResponse()
            {
                code = StatusCode.SUCCESS.ToString(),
                data = new
                {
                    accessToken = accessToken,
                    refreshToken = refreshToken
                }
            };
        }
        public async Task<AddressDto> CheckAddressBelongToUser(string userId, string addressId)
        {
            var address = await _addressRepository.GetByUserIdAsync(userId);
            return address.Where(ad => ad.Id.ToString().Equals(addressId)).FirstOrDefault();
        }
    }
}
