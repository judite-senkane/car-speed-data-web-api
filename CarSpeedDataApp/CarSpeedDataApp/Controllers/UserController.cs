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
		public IActionResult GetDayAverage(DateTime day)
		{
			var data = _carSeedDataService.GetDay(day);
			var result = new AverageSpeedResponse
			{
				AverageSpeedData = data
			};
			return Ok(result);
		}

		[Route("data")]
		[HttpGet]
		public IActionResult GetData(int page, DateTime? dateFrom, DateTime? dateTo, int? speed)
		{
			var result = _carSeedDataService.GetData(page, dateFrom, dateTo, speed);

			return Ok(result);
		}
	}
}