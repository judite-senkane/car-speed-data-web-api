using CarSpeedDataApp.Core.Models;
using CarSpeedDataApp.Core.Services;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CarSpeedDataApp.Controllers
{
	[ApiController]
	[Route("[controller]")]

	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class FileUploadController : ControllerBase
	{
		private readonly ICarSpeedDataService _carSpeedDataService;
		private readonly IFileProcessingService _fileProcessingService;
		private List<CarSpeedData> _carSpeedData;

		public FileUploadController(ICarSpeedDataService carSpeedDataService, IFileProcessingService fileProcessingService)
		{
			_carSpeedDataService = carSpeedDataService;
			_fileProcessingService = fileProcessingService;
		}

		[Route("upload")]
		[HttpPost]
		public async Task<IActionResult> UploadFile(IFormFile file)
		{
			if (file == null)
			{
				return BadRequest("Invalid file upload");
			}

			try
			{
				_carSpeedData = _fileProcessingService.ExtractDataFromFile(file).Result;

			}
			catch (FormatException)
			{
				return BadRequest("File data is not in the correct format");
			}

			_carSpeedDataService.CreateList(_carSpeedData);
			return Created("", "");
		}
	}
}
