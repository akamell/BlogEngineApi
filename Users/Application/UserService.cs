using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlogEngineApi.User.Application
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        public UserService(IConfiguration config)
        {
            _config = config;
        }

        public AuthenticationResponse DoLogin(UserRequest user)
        {
            string role = "";
            if (user.UserName == "Writer") role = "Writer";
            else if (user.UserName == "Editor") role = "Editor";
            else if (user.UserName == "Admin") role = "Admin";
            else return new AuthenticationResponse() { Code = 401 };

            try
            {
                return GenerateToken(user.UserName, user.UserName, role);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
                return new AuthenticationResponse()
                {
                    Code = 401
                };
            }
        }

        private AuthenticationResponse GenerateToken(string userName, string fullName, string role)
        {
            // Console.WriteLine($"{userName} - {fullName} - {role}");
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]{
                new Claim("fullName", fullName),
                new Claim("role", role),
                new Claim(ClaimTypes.Name, userName),
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            int minutes = 30;
            int.TryParse(_config["Jwt:ExpiryTime"], out minutes);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(minutes),
                signingCredentials: credentials
            );

            var response = new AuthenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserName = userName,
                Code = 200
            };
            return response;
        }
    }
}