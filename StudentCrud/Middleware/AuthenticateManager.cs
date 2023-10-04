using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudentCrud.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentCrud.Middleware
{
    public class AuthenticateManager
    {
       
        public AuthenticateManager()
        {

        }

        public async Task<bool> VerifyCredentials(IdentityUser user, UserManager<IdentityUser> _userManager, LoginModel model)
        {
            return user != null && await _userManager.CheckPasswordAsync(user, model.upassword);
        }

        public JwtSecurityToken GetToken(List<Claim> authClaims, IConfiguration _configuration)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

    }
}
