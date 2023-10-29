using CarSpeedDataApp.Core.Models;

namespace CarSpeedDataApp.Core.Services;

public interface ICarSpeedDataService
{
	public IEnumerable<CarSpeedData> GetRange(DateTime? dateFrom, DateTime? dateTo, int? speed);

	public IEnumerable<CarSpeedData> GetDay(DateTime date);

	public void Create (CarSpeedData data);

}