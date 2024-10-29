using Azure;
using Mango.ServiceCouponAPI.Data;
using Mango.ServiceCouponAPI.Models;
using Mango.ServiceCouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.ServiceCouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {

        private readonly AppDbContext _db;
        private ResponseDto _response;
        public CouponAPIController(AppDbContext db)
        {
            _db = db;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Coupon> Couponlist = new List<Coupon>();
            Couponlist = _db.Coupons.ToList();

            if (Couponlist.Any())
            {
                _response.Result = Couponlist;
                _response.IsSuccess = true;
                _response.Message = "success";
                return Ok(_response);
            }
            else
            {
                _response.Result = Empty;
                _response.IsSuccess = false;
                _response.Message = "Error while fetching";
                return BadRequest(_response);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            Coupon CouponData = new Coupon();
            CouponData = _db.Coupons.FirstOrDefault(z => z.CouponId == id);

            if (CouponData != null)
            {
                _response.Result = CouponData;
                _response.IsSuccess = true;
                _response.Message = "success";
                return Ok(_response);
            }
            else
            {
                _response.Result = CouponData;
                _response.IsSuccess = false;
                _response.Message = "Error while fetching";
                return BadRequest(_response);
            }
        }


        [HttpGet]
        [Route("{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            Coupon CouponData = new Coupon();
            CouponData = _db.Coupons.FirstOrDefault(z => z.CouponCode == code.ToString());

            if (CouponData.CouponCode != null)
            {
                _response.Result = CouponData;
                _response.IsSuccess = true;
                _response.Message = "success";
                return Ok(_response);
            }
            else
            {
                _response.Result = CouponData;
                _response.IsSuccess = false;
                _response.Message = "Error while fetching";
                return BadRequest(_response);
            }
        }


        [HttpPost("AddCoupon")]
        public async Task<IActionResult> AddCoupon([FromBody] Coupon coupon)
        {
            try
            {
                _db.Coupons.Add(coupon);
                _db.SaveChanges();

                return Ok(_response);
            }
            catch
            {
                return BadRequest(_response);
            }
        }


        [HttpPut("UpdateCoupon")]
        public async Task<IActionResult> UpdateCoupon([FromBody] Coupon coupon)
        {
            try
            {
                _db.Coupons.Update(coupon);
                _db.SaveChanges();

                return Ok(_response);
            }
            catch
            {
                return BadRequest(_response);
            }
        }



        [HttpDelete("DeleteCoupon")]
        public async Task<IActionResult> DeleteCoupon( int couponid)
        {
            try
            {
                var coupondata=_db.Coupons.First(u=>u.CouponId==couponid);
                _db.Coupons.Remove(coupondata);
                _db.SaveChanges();

                return Ok(_response);
            }
            catch
            {
                return BadRequest(_response);
            }
        }
    }
}
