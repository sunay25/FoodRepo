using Mango.Service.AuthAPI.Models;
using Mango.Service.AuthAPI.Service.IService;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mango.Service.AuthAPI.Service
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly jwtOption _jwtOption;

        public JwtTokenGenerator( IOptions <jwtOption> jwtOption)
        {
            _jwtOption = jwtOption.Value;
        }
        public string GenerateToken(ApplicationUser applicationUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOption.Secret);

            var claimList = new List<Claim> 
            { 
                new Claim(JwtRegisteredClaimNames.Email,applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub,applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Name,applicationUser.Name)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOption.Audience,
                Issuer = _jwtOption.Issuser,
                Subject = new ClaimsIdentity(claimList),
                Expires=DateTime.UtcNow.AddDays(7),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
