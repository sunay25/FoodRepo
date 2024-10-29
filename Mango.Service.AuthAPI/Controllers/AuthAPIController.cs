using Mango.Service.AuthAPI.Models;
using Mango.Service.AuthAPI.Models.Dto;
using Mango.Service.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Service.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        protected ResponseDto _responseDto;
        private readonly IAuthService _authService;

        public AuthAPIController(IAuthService authService)
        {
            _responseDto= new();
            _authService = authService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationDto)
        {
            var errormeassage = await _authService.Registration(registrationDto);

            if (!string.IsNullOrEmpty(errormeassage))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = errormeassage;
                return BadRequest(_responseDto);
            }
            else
                return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginresponse = await _authService.Login(loginRequestDto);

            if (loginresponse.User== null || loginresponse.token == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Invalid Crediantial";
                return BadRequest(_responseDto);
            }
            else
            {
                _responseDto.Result = loginresponse;
                return Ok(_responseDto);
            }
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var response = await _authService.AssignRole(registrationRequestDto.Email, registrationRequestDto.roleName.ToUpper());

            if(!response)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Invalid Assignmanet of role";
                return BadRequest(_responseDto);
            }
            else
            {
                _responseDto.Result = response;
                return Ok(response);
            }


        }
    }
}
