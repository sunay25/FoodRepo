using Mango.Service.AuthAPI.Models;

namespace Mango.Service.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        public  string GenerateToken(ApplicationUser applicationUser);
    }
}
