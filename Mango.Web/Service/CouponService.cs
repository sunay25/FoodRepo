using Mango.Web.Models.Dto;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using System.Collections.Specialized;

namespace Mango.Web.Service
{
    public class CouponService : ICouponService
    {   
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService) 
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateCouponsAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType=SD.ApiType.POST,
                Data=couponDto,
                Url= SD.CouponAPIBase + "/api/coupon/AddCoupon"

            });
        }

        public async Task<ResponseDto?> DeleteCouponsAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponAPIBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            string st = Convert.ToString(SD.ApiType.GET);
            string st2 = SD.CouponAPIBase + "/api/coupon";
            ResponseDto responseDto = new();

			RequestDto requestDto = new()
            {

                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon"
            };
            try
            {
                responseDto = await _baseService.SendAsync(requestDto);
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }

            return responseDto;
			
        }

        public async Task<ResponseDto?> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/GetByCode/" + couponCode
            });
        }

        public async Task<ResponseDto?> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponseDto?> UpdateCouponsAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = couponDto,
                Url = SD.CouponAPIBase + "/api/coupon"
            });
        }
    }
}
