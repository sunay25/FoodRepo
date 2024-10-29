namespace Mango.Service.AuthAPI.Models.Dto
{
    public class LoginResponseDto
    {
        public  UserDto User { get; set; }
        
        public string token { get; set; }
    }
}
