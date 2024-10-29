using Microsoft.AspNetCore.Mvc;
using Mango.Web.Models.Dto;
using Mango.Web.Service.IService;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
		private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
			_couponService=couponService;

		}
		public async Task<IActionResult> CouponIndex()
		{
			List<CouponDto>? list = new();
			try
			{
				ResponseDto? response = await _couponService.GetAllCouponsAsync();
				if (response != null && response.IsSuccess)
				{
					list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
				}
				else
				{
					TempData["error"] = response?.Message;
				}

			}
			catch (Exception ex)
			{
				string message = ex.Message;
			}

			
			return View(list);
		}

		public async Task<IActionResult> CouponCreate()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CouponCreate(CouponDto model)
		{
			ResponseDto? response = await _couponService.CreateCouponsAsync(model);
			if (response != null && response.IsSuccess)
			{
				//list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
			}
			else
			{
				TempData["error"] = response?.Message;
			}

		  return RedirectToAction(nameof(CouponIndex));
        }


	}
}
