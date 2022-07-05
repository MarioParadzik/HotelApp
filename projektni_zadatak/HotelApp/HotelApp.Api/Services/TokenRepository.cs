using HotelApp.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelApp.Api.Services
{
    public class TokenRepository : ITokenRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration configuration;
        public TokenRepository(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            this.configuration = configuration;
        }
        public async Task<string> GenerateToken(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AuthKey:key").Value)),
                        SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
