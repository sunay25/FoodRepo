namespace Mango.Service.AuthAPI.Models
{
    public class jwtOption
    {
        public string Secret { get; set; } =string.Empty;
        public string Issuser { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;


    }
}
