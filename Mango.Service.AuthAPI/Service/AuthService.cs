using Mango.Service.AuthAPI.Models;
using Mango.Service.AuthAPI.Models.Dto;
using Mango.Service.AuthAPI.Service.IService;
using Mango.ServiceCouponAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace Mango.Service.AuthAPI.Service
{
    public class AuthService : IAuthService
    {

        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager=roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user =_db.ApplicationUsers.FirstOrDefault(x=>x.UserName==email);

            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                    await _userManager.AddToRoleAsync(user, roleName);
                    return true;

                }

               

            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {

            var user = _db.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDto.userName.ToLower());

            bool isvalid = await _userManager.CheckPasswordAsync(user, loginRequestDto.password);

            if (user == null || isvalid == false)
            {
                return new LoginResponseDto() { User = null, token = "" };
            }
            else
            {

                UserDto userdetails = new()
                {
                    Email = user.Email,
                    ID = user.Id,
                    PhoneNumber = user.PhoneNumber,
                    Name = user.Name
                };

                var token_value = _jwtTokenGenerator.GenerateToken(user);
                return new LoginResponseDto() { User = userdetails, token = token_value };
            }


        }

        public async Task<string> Registration(RegistrationRequestDto registrationDto)
        {
            string error_message = "";
            ApplicationUser user = new ApplicationUser();
            user.UserName = registrationDto.Email;
            user.Email = registrationDto.Email;
            user.NormalizedEmail = registrationDto.Email.ToUpper();
            user.PhoneNumber=registrationDto.PhoneNumber;
            user.Name= registrationDto.Name;

            try {
                var result = await _userManager.CreateAsync(user, registrationDto.Password);

                if(result.Succeeded)
                {
                    var userToReturn=_db.ApplicationUsers.FirstOrDefault(m =>m.UserName==registrationDto.Email);

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        PhoneNumber=userToReturn.PhoneNumber,
                        Name = userToReturn.Name
                    };
                    return "";
                }
                else
                {
                    error_message = result.Errors.FirstOrDefault().Description;
                    return  error_message;
                }
                
                }
            catch {
            }

            return error_message;
        }
    }
}
