using CarSpeedDataApp.Core.Models;
using CarSpeedDataApp.Core.Services;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace CarSpeedDataApp.Controllers
{
	[ApiController]
	[Route("[controller]")]

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
			string uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
			Directory.CreateDirectory(uploadDirectory);
			string path = Path.Combine(uploadDirectory, file.FileName);

			using (Stream stream = file.OpenReadStream())
			{
				using (FileStream fileStream = new FileStream(path, FileMode.Create))
				{
					await stream.CopyToAsync(fileStream);
				}
			}

			var rowSplit = System.IO.File.ReadAllLines(path).ToList();

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
