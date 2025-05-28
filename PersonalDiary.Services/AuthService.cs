using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PersonalDiary.Business.Models.Users;
using PersonalDiary.HttpRepositories.Contracts;
using PersonalDiary.Services.Contracts;

namespace PersonalDiary.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;
        private readonly IConfiguration configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            this.authRepository = authRepository;
            this.configuration = configuration;
        }

        public async Task<SignInResponseModel> SignInAsync(SignInRequestModel request)
        {
            var user = await this.authRepository.ValidateUserCredentialsAsync(request.Username, request.Password);

            if (user == null)
            {
                return new SignInResponseModel
                {
                    Success = false,
                    Message = "Invalid username or password"
                };
            }

            if (user.UserName == "Admin")
            {
                user.UserPermissions = Entities.ENUMS.PERMISSIONS.Admin;
            }

            // Generate JWT token
            var token = this.GenerateJwtToken(user);

            return new SignInResponseModel
            {
                Success = true,
                Message = "Successfully signed in",
                Token = token,
                User = user
            };
        }

        private string GenerateJwtToken(UserBiz user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(this.configuration["JwtSettings:ExpirationInDays"]));

            var token = new JwtSecurityToken(
                issuer: this.configuration["JwtSettings:Issuer"],
                audience: this.configuration["JwtSettings:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
