using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace phone_shop_server.Util
{
    public interface IJwtUtil
    {
        bool isTokenExpired(string? token);
        string? getUserIdFromToken(string token);
        string? getUserNameFromToken(string token);
        string? getTokenFromHeader(HttpContext context);
        JwtSecurityToken GenerateResetPasswordApiToken(string userid);
        JwtSecurityToken GenerateAccessToken(List<Claim> authClaims, string userid);
        JwtSecurityToken GenerateRefreshToken(List<Claim> authClaims, string userid);
    }
    public class JwtUtil : IJwtUtil
    {
        private readonly IConfiguration configuration;
        public JwtUtil(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }
        public bool isTokenExpired(string? token)
        {
            if (token == null) return true;
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var x = DateTime.UtcNow.AddHours(7).AddDays(30);
            if (jwt == null)
                return true;
            var expired = jwt.ValidTo;
            if (expired > DateTime.UtcNow.AddHours(7))
                return false;
            return true;

        }
        public string? getUserNameFromToken(string token)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            if (jwt == null)
                return "jwt is null";
            var userId = jwt.Claims.FirstOrDefault(c => c.Type == "UserName");
            if (userId == null)
                return "username is null";
            return userId.Value;
        }
        public string? getUserIdFromToken(string token)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            if (jwt == null)
                return "jwt is null";
            var userId = jwt.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userId == null)
                return "userid is null";
            return userId.Value;
        }
        public string? getTokenFromHeader(HttpContext context)
        {
            var listAuth = context.Request.Headers.Authorization.ToList();
            foreach (var item in listAuth)
            {
                if (item.Split(' ')[0].Equals("Bearer"))
                    return item.Split(" ")[1];
            }
            return null;
        }
        public JwtSecurityToken GenerateResetPasswordApiToken(string userid)
        {
            List<Claim> authClaims = new List<Claim>();
            authClaims.Add(new Claim(type: "ResetPassword", "true"));
            authClaims.Add(new Claim(type: "UserName", value: userid));
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfig:RefreshPasswordSecret"]));
            var token = new JwtSecurityToken(
                issuer: configuration["JWTConfig:ValidIssuer"],
                audience: configuration["JWTConfig:ValidAudience"],
                expires: DateTime.UtcNow.AddHours(7).AddMinutes(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        public JwtSecurityToken GenerateAccessToken(List<Claim> authClaims, string userid)
        {
            authClaims.Add(new Claim(type: "AccessToken", "true"));
            authClaims.Add(new Claim(type: "UserId", value: userid));
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfig:AccessSecret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWTConfig:ValidIssuer"],
                audience: configuration["JWTConfig:ValidAudience"],
                expires: DateTime.UtcNow.AddHours(7).AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        public JwtSecurityToken GenerateRefreshToken(List<Claim> authClaims, string userid)
        {
            authClaims.Add(new Claim(type: "RefreshToken", "true"));
            authClaims.Add(new Claim(type: "UserName", value: userid));
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfig:RefreshSecret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWTConfig:ValidIssuer"],
                audience: configuration["JWTConfig:ValidAudience"],
                expires: DateTime.UtcNow.AddHours(7).AddDays(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
