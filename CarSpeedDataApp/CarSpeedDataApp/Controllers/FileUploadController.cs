using AutoMapper;
using CarSpeedDataApp.Core.Models;
using CarSpeedDataApp.Core.Services;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CarSpeedDataApp.Controllers
{
	[Route("[controller]")]
	[ApiController]

	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class FileUploadController : ControllerBase
	{
		private readonly ICarSpeedDataService _carSpeedDataService;

		public FileUploadController(ICarSpeedDataService carSpeedDataService)
		{
			_carSpeedDataService = carSpeedDataService;
		}

		[Route("upload")]
		[HttpPost]
		public async Task<IActionResult> UploadFile(IFormFile file)
		{
			await using var stream = file.OpenReadStream();
			using var reader = new StreamReader(stream);
			var allData = await reader.ReadToEndAsync();
			var rowSplit = allData.Split("\n").Where(l => l.Length > 0).ToList();

			List<CarSpeedData> allCarData = new List<CarSpeedData>();

			var lastLine = rowSplit.Last();

			foreach (var line in rowSplit)
			{
				string[] columns = line.Split("\t");

				var data = new CarSpeedData
				{
					DateAndTime = DateTime.Parse(columns[0]),
					SpeedKmH = int.Parse(columns[1]),
					LicenseNumber = columns[2],
				};

				allCarData.Add(data);
			}

			_carSpeedDataService.CreateList(allCarData);
			return Created("", "");
		}
	}
}
