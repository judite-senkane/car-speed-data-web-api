using CarSpeedDataApp.Core.Models;

namespace CarSpeedDataApp
{
	public class DataResult
	{
		public List<CarSpeedData> Items {get; set; }
		public int TotalPages { get; set; }
	}
}
