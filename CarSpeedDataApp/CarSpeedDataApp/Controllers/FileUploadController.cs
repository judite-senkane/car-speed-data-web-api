using AutoMapper;
using CarSpeedDataApp.Core.Models;
using CarSpeedDataApp.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSpeedDataApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FileUploadController : ControllerBase
	{
		private readonly ICarSpeedDataService _carSpeedDataService;
		private readonly IMapper _mapper;

		public FileUploadController(ICarSpeedDataService carSpeedDataService, IMapper mapper)
		{
			_carSpeedDataService = carSpeedDataService;
			_mapper = mapper;
		}

		[HttpPost ("upload")]
		public async Task<IActionResult> UploadFile(IFormFile file)
		{
			if (file == null || file.Length == 0)
			{
				return BadRequest("Invalid file.");
			}

			await using (var stream = file.OpenReadStream())
			{
				using (var reader = new StreamReader(stream))
				{

					List<CarSpeedDataRequest> parsedData = new List<CarSpeedDataRequest>();
					string? line;

					while ((line = await reader.ReadLineAsync()) != null)
					{
						string[] columns = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

						if (columns.Length >= 4)
						{
							var data = new CarSpeedDataRequest
							{
								DateAndTime = DateTime.Parse(columns[0] + " " + columns[1]),
								SpeedKmH = int.Parse(columns[2]),
								LicenseNumber = columns[3],
							};

							parsedData.Add(data);
						}

						else
						{
							return BadRequest("Incorrect file format");
						}

						foreach (var request in parsedData)
						{
							var carData = _mapper.Map<CarSpeedData>(request);
							_carSpeedDataService.Create(carData);
						}

					}
				}
			}

			return Created("file uploaded", "");

		}

	}
}
