using CarSpeedDataApp.Core.Models;
using CarSpeedDataApp.Core.Services;
using Microsoft.AspNetCore.Http;

namespace CarSpeedDataApp.Services
{
	public class FileProcessingService : IFileProcessingService
	{
		private string _path;
		public async Task<List<CarSpeedData>> ExtractDataFromFile(IFormFile file)
		{
			await SaveFileInDirectory(file);

			var rowSplit = File.ReadAllLines(_path).ToList();

			List<CarSpeedData> allCarData = new List<CarSpeedData>();

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
			return allCarData;
		}

		private async Task SaveFileInDirectory(IFormFile file)
		{
			string uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
			Directory.CreateDirectory(uploadDirectory);
			_path = Path.Combine(uploadDirectory, file.FileName);

			using (Stream stream = file.OpenReadStream())
			{
				using (FileStream fileStream = new FileStream(_path, FileMode.Create))
				{
					await stream.CopyToAsync(fileStream);
				}
			}
		}
	}
}
