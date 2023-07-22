using Microsoft.AspNetCore.Identity;
using phone_shop_server.Database.Entity;
using phone_shop_server.Database.Repository;
using phone_shop_server.Util;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace phone_shop_server.Business.Service
{
    public class UserService
    {
        private readonly IJwtUtil _jwtUtil;
        private readonly IMailUtil _mailUtil;
        private readonly IConfiguration _configuration;
        private readonly IFileUtil _fileUtil;
        private readonly IUserRepository _userRepository;
        public UserService(
            IJwtUtil jwtUtil,
            IMailUtil sendMailUtil,
            IConfiguration configuration,
            IUserRepository userRepository,
            IFileUtil uploadFileUtil
            )
        {
            _jwtUtil = jwtUtil;
            _mailUtil = sendMailUtil;
            _configuration = configuration;
            _fileUtil = uploadFileUtil;
            _userRepository = userRepository;
        }
        public async Task<string?> GetUserIdFromToken(string token)
        {
            string userName = jwtUtil.getUserNameFromToken(token);
            if (userName == null)
            {
                return null;
            }
            return (await userRepository.findUserByEmailAsync(userName)).Id;

        }
        public async Task<UserDto?> getDetailAsync(string userName)
        {
            var user = await userRepository.findUserByEmailAsync(userName);
            if (user != null)
            {
                var roles = await userRepository.GetListRoleOfUser(user.Id);
                return new UserDto()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Age = user.Age,
                    Avatar = user.Avatar,
                    PhoneNumber = user.PhoneNumber,
                    Roles = roles.ToList()
                };
            }
            return null;
        }
        public async Task<UserDto> GetByUserIdAsync(string userId)
        {
            try
            {
                AppUser user = await userRepository.FindByIdAsync(userId);
                if (user != null)
                {
                    return new UserDto()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Age = user.Age,
                        Avatar = user.Avatar,
                        PhoneNumber = user.PhoneNumber
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<AppUser> FindByIdAsync(string userId)
        {
            return await userRepository.FindByIdAsync(userId);
        }
        public async Task<IDeleteUserInteractor.Response> DeleteAsync(IDeleteUserInteractor.Request request)
        {
            string userId = await GetUserIdFromToken(request.token);
            if (userId == null || userId == request.deleteId)
            {
                return new IDeleteUserInteractor.Response()
                {
                    Message = "Delete user failed",
                    Success = false
                };
            }
            var result = await userRepository.DeleteAsync(request.deleteId);
            if (result)
            {
                return new IDeleteUserInteractor.Response()
                {
                    Message = "Delete user success",
                    Success = true
                };
            }
            return new IDeleteUserInteractor.Response()
            {
                Message = "Delete user failed",
                Success = false
            };
        }
        public async Task<IUpdateUserInteractor.Response> UpdateAsync(IUpdateUserInteractor.Request request)
        {
            try
            {
                UpdateUserDto updateUserDto = request.updateUserDto;
                string userId = await GetUserIdFromToken(request.token);
                AppUser user = await userRepository.findUserByEmailAsync(updateUserDto.Email);
                if (user != null && user.Id != userId)
                    return new IUpdateUserInteractor.Response()
                    {
                        Success = true,
                        Message = "Email already used by another user"
                    };
                string? avatar = "";
                AppUser updateUser = await userRepository.FindByIdAsync(userId);
                if (updateUserDto.Avatar != null)
                {
                    avatar = await uploadFileUtil.UploadAsync(updateUserDto.Avatar);
                    updateUser.Avatar = avatar;
                }
                updateUser.Age = updateUserDto.Age;
                updateUser.Email = updateUserDto.Email;
                updateUser.LastName = updateUserDto.LastName;
                updateUser.FirstName = updateUserDto.FirstName;
                updateUser.PhoneNumber = updateUserDto.PhoneNumber;
                var isSuccess = await userRepository.updateUser(updateUser);
                if (isSuccess)
                    return new IUpdateUserInteractor.Response()
                    {
                        Success = true,
                        Message = "Update user success"
                    };
                return new IUpdateUserInteractor.Response()
                {
                    Success = false,
                    Message = "Update user failed"
                };
            }
            catch (Exception ex)
            {
                return new IUpdateUserInteractor.Response()
                {
                    Success = false,
                    Message = "Error occur when update user"
                };
            }
        }
        public async Task<IUserLoginInteractor.Response> LoginService(IUserLoginInteractor.Request request)
        {
            var result = await userRepository.LoginRepository(request.loginDto);
            if (result == null)
            {
                return new IUserLoginInteractor.Response(null, null, "login failed", false);
            }
            var li = result.ToList();
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtUtil.GenerateAccessToken(li, request.loginDto.UserName));
            var refreshToken = new JwtSecurityTokenHandler().WriteToken(jwtUtil.GenerateRefreshToken(li, request.loginDto.UserName));
            var user = await userRepository.findUserByEmailAsync(request.loginDto.UserName);
            user.RefreshToken = refreshToken;
            await userRepository.updateUser(user);
            return new IUserLoginInteractor.Response(accessToken, refreshToken, "login success", true);
        }
        public async Task<IGetAllUserInteractor.Response> GetAllUser()
        {
            var users = await userRepository.GetAll();
            List<UserDto> dtos = new List<UserDto>();
            foreach (var user in users)
            {
                var roles = await userRepository.GetListRoleOfUser(user.Id);
                dtos.Add(new UserDto()
                {
                    Age = user.Age,
                    Avatar = user.Avatar,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Id = user.Id,
                    IsBlock = user.IsBlock,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Roles = roles.ToList()
                });
            }
            return new IGetAllUserInteractor.Response()
            {
                Success = true,
                Message = "Get all user success",
                Users = dtos
            };

        }
        public async Task<IAddRoleToUserInteractor.Response> AddRoleToUser(string userId, string role)
        {
            if (await userRepository.IsContainRole(userId, role))
            {
                return new IAddRoleToUserInteractor.Response()
                {
                    Success = false,
                    Message = "User already has this role"
                };
            }
            var success = await userRepository.AddRole(userId, role);
            if (!success)
            {
                return new IAddRoleToUserInteractor.Response()
                {
                    Success = false,
                    Message = "Add role to user failed service"
                };
            }
            var user = await userRepository.FindByIdAsync(userId);
            var tmpRoles = await userRepository.GetListRoleOfUser(userId);
            return new IAddRoleToUserInteractor.Response()
            {
                Success = true,
                Message = "Add role to user success",
                userDto = new UserDto()
                {
                    Age = user.Age,
                    Avatar = user.Avatar,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Id = user.Id,
                    IsBlock = user.IsBlock,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Roles = tmpRoles.ToList()
                }
            };
        }
        public async Task<IAddRoleToUserInteractor.Response> RemoveRoleFromUser(string userId, string role)
        {
            if (!(await userRepository.IsContainRole(userId, role)))
            {
                return new IAddRoleToUserInteractor.Response()
                {
                    Success = false,
                    Message = "User don't have this role"
                };
            }
            var user = await userRepository.FindByIdAsync(userId);
            var success = await userRepository.removeUserRoleAsync(user, role);
            if (!success)
            {
                return new IAddRoleToUserInteractor.Response()
                {
                    Success = false,
                    Message = "Remove role to user failed"
                };
            }
            var tmpRoles = await userRepository.GetListRoleOfUser(userId);
            return new IAddRoleToUserInteractor.Response()
            {
                Success = true,
                Message = "Remove role to user success",
                userDto = new UserDto()
                {
                    Age = user.Age,
                    Avatar = user.Avatar,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Id = user.Id,
                    IsBlock = user.IsBlock,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Roles = tmpRoles.ToList()
                }
            };
        }
        public async Task<IUserSignupInteractor.Response> SignupService(IUserSignupInteractor.Request request)
        {
            var dto = request.dto;
            var userExist = await userRepository.userExist(request.dto.UserName);
            if (userExist == null)
            {
                AppUser user = new AppUser()
                {
                    Email = dto.UserName,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = dto.UserName,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                };
                var result = await userRepository.createUserAsync(user, dto.Password);
                if (!result.Succeeded)
                    return new IUserSignupInteractor.Response("Can't create user", false);
                if (!await userRepository.roleExist(DbUserRole.User))
                    await userRepository.createRoleAsync(new IdentityRole(DbUserRole.User));
                if (!await userRepository.roleExist(DbUserRole.Admin))
                {
                    await userRepository.createRoleAsync(new IdentityRole(DbUserRole.Admin));
                }
                if (!await userRepository.roleExist(DbUserRole.Owner))
                {
                    await userRepository.createRoleAsync(new IdentityRole(DbUserRole.Owner));
                }
                //role user is role default
                await userRepository.addUserRoleAsync(user, DbUserRole.User);
                if (!request.dto.Role.Contains(DbUserRole.User) && !request.dto.Role.Contains(DbUserRole.User)
                    && !request.dto.Role.Contains(DbUserRole.User))
                {
                    return new IUserSignupInteractor.Response("User must has role (user, admin or owner)", false);
                }
                foreach (string userRole in request.dto.Role)
                {
                    switch (userRole)
                    {
                        case DbUserRole.User:
                            await userRepository.addUserRoleAsync(user, DbUserRole.User);
                            break;
                        case DbUserRole.Admin:
                            await userRepository.addUserRoleAsync(user, DbUserRole.Admin);
                            break;
                        case DbUserRole.Owner:
                            await userRepository.addUserRoleAsync(user, DbUserRole.Owner);
                            break;
                    }
                }
                return new IUserSignupInteractor.Response("Create user success", true);
            }
            return new IUserSignupInteractor.Response("User is already exists", false);
        }
        public async Task<IFogotPasswordInteractor.Response> fogotPasswordService(IFogotPasswordInteractor.Request request)
        {
            var user = await userRepository.findUserByEmailAsync(request.Email);
            if (user == null)
                return new IFogotPasswordInteractor.Response("Email is not exists", false);
            else if (!jwtUtil.isTokenExpired(user.ResetPasswordToken))
            {
                return new IFogotPasswordInteractor.Response("We already sent to your account! Please try again after 3 minute", false);
            }
            var token = new JwtSecurityTokenHandler().WriteToken(jwtUtil.GenerateResetPasswordApiToken(user.UserName));
            var result = await sendMailUtil.SendMailAsync(user.Email, "Please click to the link bellow to reset your password",
                $"{request.Url}/{token}");
            if (result.Success == true) await userRepository.updateResetPasswordTokenAsync(user.UserName, token);
            return new IFogotPasswordInteractor.Response(result.Message, result.Success);
        }
        public async Task<IResetPasswordInteractor.Response> resetPasswordService(IResetPasswordInteractor.Request request)
        {
            string userName = jwtUtil.getUserNameFromToken(request.token);
            bool isSuccess = await userRepository.updatePassword(userName, request.password);
            if (isSuccess)
            {
                return new IResetPasswordInteractor.Response()
                {
                    Message = "update new password success",
                    Success = true
                };
            }
            return new IResetPasswordInteractor.Response()
            {
                Message = "failed orcurr when update password",
                Success = false
            };
        }
        public async Task<IChangePasswordInteractor.Response> changePasswordService(IChangePasswordInteractor.Request request)
        {
            string userId = await GetUserIdFromToken(request.token);
            if (userId == null)
                return new IChangePasswordInteractor.Response()
                {
                    Success = false,
                    Message = "Change password failed"
                };
            bool isSuccess = await userRepository.ChangePassword(userId, request.oldPassword, request.newPassword);
            if (isSuccess)
                return new IChangePasswordInteractor.Response()
                {
                    Success = true,
                    Message = "Change password success"
                };
            return new IChangePasswordInteractor.Response()
            {
                Success = false,
                Message = "Change password failed"
            };
        }
        public async Task<IBlockAndUnlockUserInteractor.Response> BlockUserAsync(IBlockAndUnlockUserInteractor.Request request)
        {
            string userId = await GetUserIdFromToken(request.token);
            List<string> userRoles = (await userRepository.GetListRoleOfUser(userId)).ToList();
            List<string> blockUuserRoles = (await userRepository.GetListRoleOfUser(request.userId)).ToList();
            if (userRoles.Count == 0 || blockUuserRoles.Count == 0
                || !userRoles.Contains(DbUserRole.Owner) || blockUuserRoles.Contains(DbUserRole.Owner))
            {
                return new IBlockAndUnlockUserInteractor.Response()
                {
                    Success = false,
                    Message = "User don't have permission!"
                };
            }
            await userRepository.BlockAsync(request.userId);
            return new IBlockAndUnlockUserInteractor.Response()
            {
                Success = true,
                Message = "Block user success"
            };
        }
        public async Task<IBlockAndUnlockUserInteractor.Response> UnlockUserAsync(IBlockAndUnlockUserInteractor.Request request)
        {
            string userId = await GetUserIdFromToken(request.token);
            List<string> userRoles = (await userRepository.GetListRoleOfUser(userId)).ToList();
            List<string> blockUuserRoles = (await userRepository.GetListRoleOfUser(request.userId)).ToList();
            if (userRoles.Count == 0 || blockUuserRoles.Count == 0
                || !userRoles.Contains("owner"))
            {
                return new IBlockAndUnlockUserInteractor.Response()
                {
                    Success = false,
                    Message = "User don't have permission!"
                };
            }
            await userRepository.UnlockAsync(request.userId);
            return new IBlockAndUnlockUserInteractor.Response()
            {
                Success = true,
                Message = "Unlock user success"
            };
        }
        public async Task<IRefreshTokenInteractor.Response> RefreshToken(string token)
        {
            string userId = await GetUserIdFromToken(token);
            if (userId == null)
            {
                return new IRefreshTokenInteractor.Response()
                {
                    Success = false,
                    Message = "Token is invalid"
                };
            }
            AppUser user = await userRepository.FindByIdAsync(userId);
            if (user.RefreshToken != null && user.RefreshToken != token)
            {
                return new IRefreshTokenInteractor.Response()
                {
                    Success = false,
                    Message = "Token is invalid"
                };
            }
            var liRoles = await userRepository.GetListRoleOfUser(user.Id);
            List<Claim> userRoles = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var role in liRoles)
            {
                userRoles.Add(new Claim(ClaimTypes.Role, role));
            }
            string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtUtil.GenerateAccessToken(userRoles, user.UserName));
            string refreshToken = new JwtSecurityTokenHandler().WriteToken(jwtUtil.GenerateRefreshToken(userRoles, user.UserName));
            user.RefreshToken = refreshToken;
            await userRepository.updateUser(user);
            return new IRefreshTokenInteractor.Response()
            {
                Success = true,
                Message = "success",
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
