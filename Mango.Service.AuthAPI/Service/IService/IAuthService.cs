using Mango.Service.AuthAPI.Models;
using Mango.Service.AuthAPI.Models.Dto;

namespace Mango.Service.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Registration(RegistrationRequestDto registrationDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        Task<bool> AssignRole(string email,string roleName);

    }
}
