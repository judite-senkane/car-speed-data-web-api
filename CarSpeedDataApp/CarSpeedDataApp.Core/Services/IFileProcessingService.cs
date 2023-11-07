using CarSpeedDataApp.Core.Models;
using Microsoft.AspNetCore.Http;

namespace CarSpeedDataApp.Core.Services
{
	public interface IFileProcessingService
	{
		Task<List<CarSpeedData>> ExtractDataFromFile(IFormFile file);
	}
}
