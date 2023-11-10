using System.Web.Http.Cors;
using CarSpeedDataApp.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarSpeedDataApp.Controllers
{
	[ApiController]
	[Route("[controller]")]

	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class UserController : ControllerBase
	{
		private readonly ICarSpeedDataService _carSeedDataService;

		public UserController(ICarSpeedDataService carSpeedDataService)
		{
			_carSeedDataService = carSpeedDataService;
		}

		[Route("day-speed-average")]
		[HttpGet]
		public async Task<IActionResult> GetDayAverage(DateTime day)
		{
			var data = await _carSeedDataService.GetDay(day);
			if (data == null) return NotFound();

			return Ok(data);
		}

		[Route("data")]
		[HttpGet]
		public async Task<IActionResult> GetData(int? page, DateTime? dateFrom, DateTime? dateTo, int? speed)
		{
			var result = await _carSeedDataService.GetData(page, dateFrom, dateTo, speed);
			if (result == null) return NotFound();

			return Ok(result);
		}
	}
}