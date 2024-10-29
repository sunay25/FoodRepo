using static Mango.Web.Utility.SD;

namespace Mango.Web.Models.Dto
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; }
        public string Url { get; set; }
        public object? Data { get; set; }
        public string token { get; set; }
		public enum ContentType
		{
			Json,
			MultipartFormData,
		}
	}
}
