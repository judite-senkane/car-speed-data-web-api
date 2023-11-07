using CarSpeedDataApp.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarSpeedDataApp.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class CleanupController : ControllerBase
	{
		private readonly ICarSpeedDataService _carSpeedDataService;

		public CleanupController(ICarSpeedDataService carSpeedDataService)
		{
			_carSpeedDataService = carSpeedDataService;
		}

		[Route("clear")]
		[HttpPost]
		public IActionResult ClearData()
		{
			_carSpeedDataService.ClearDatabase();
			return Ok();
		}
	}
}
