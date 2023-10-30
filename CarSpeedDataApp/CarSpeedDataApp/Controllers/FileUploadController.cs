using AutoMapper;
using CarSpeedDataApp.Core.Models;
using CarSpeedDataApp.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarSpeedDataApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FileUploadController : ControllerBase
	{
		private readonly ICarSpeedDataService _carSpeedDataService;
		private readonly IMapper _mapper;
		//private static readonly object _controllerLock = new();

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

			await using var stream = file.OpenReadStream();
			using var reader = new StreamReader(stream);
			var allData = await reader.ReadToEndAsync();
			var rowSplit = allData.Split("\n");

			List<CarSpeedDataRequest> allCarData = new List<CarSpeedDataRequest>();

			foreach (var line in rowSplit)
			{
				while (line != null)
				{
					string[] columns = line.Split(new char[] { ' ', '\t' });

					if (columns.Length >= 4)
					{
						var data = new CarSpeedDataRequest
						{
							DateAndTime = DateTime.Parse(columns[0] + " " + columns[1]),
							SpeedKmH = int.Parse(columns[2]),
							LicenseNumber = columns[3],
						};

						allCarData.Add(data);
					}
					else
					{
						return BadRequest("Incorrect file format");
					}
				}
			}

			var lastItem = allCarData.Last();

			foreach (CarSpeedDataRequest carData in allCarData)
			{
				var mappedData = _mapper.Map<CarSpeedData>(carData);
				var mappedDataList = new List<CarSpeedData>();

				mappedDataList.Add(mappedData);

				if (mappedDataList.Count == 100 || carData.Equals(lastItem))
				{
					_carSpeedDataService.CreateList(mappedDataList);
					mappedDataList.Clear();
				}
			}

			_carSpeedDataService.SaveChanges();
			return Created("file uploaded", "");
		}
	}
}
