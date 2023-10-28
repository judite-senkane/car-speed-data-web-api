using CarSpeedDataApp.Core.Models;

namespace CarSpeedDataApp.Core.Services;

public interface ICarSpeedDataService<TCarSpeedData>
{
	public IEnumerable<CarSpeedData> GetRange(DateTime? dateFrom, DateTime? dateTo, int? speed);

	public IEnumerable<CarSpeedData> GetDay(DateTime date);
}