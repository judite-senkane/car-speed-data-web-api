using CarSpeedDataApp.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarSpeedDataApp.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		private readonly ICarSpeedDataService _carSeedDataService;

		public UserController(ICarSpeedDataService carSpeedDataService)
		{
			_carSeedDataService = carSpeedDataService;
		}

		[Route("day-speed-average")]
		[HttpGet]
		public IActionResult GetDayAverage(DateTime day)
		{
			var result = _carSeedDataService.GetDay(day);
			return Ok(result);
		}

		[Route("filter-data")]
		[HttpGet]
		public IActionResult FilterData(DateTime? dateFrom, DateTime? dateTo, int? speed)
		{
			var result = _carSeedDataService.GetRange(dateFrom, dateTo, speed);
			return Ok(result);
		}

	}
}