﻿using AutoMapper;
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

				if (allCarData.Count == 100 || line.Equals(lastLine))
				{
					_carSpeedDataService.CreateList(allCarData);
					allCarData.Clear();
				}
			}

			return Created("", "");
		}
	}
}
