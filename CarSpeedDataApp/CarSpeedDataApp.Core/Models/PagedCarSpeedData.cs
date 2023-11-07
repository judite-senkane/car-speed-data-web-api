using CarSpeedDataApp.Core.Models;

namespace CarSpeedDataApp
{
	public class PagedCarSpeedData
	{
		public List<CarSpeedData> Items { get; set; }
		public int TotalPages { get; set; }
	}
}
