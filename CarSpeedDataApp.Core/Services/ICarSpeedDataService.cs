using CarSpeedDataApp.Core.Models;

namespace CarSpeedDataApp.Core.Services;

public interface ICarSpeedDataService
{
	public IEnumerable<CarSpeedData> GetRange(DateTime? dateFrom, DateTime? dateTo, int? speed);

	public IEnumerable<double> GetDay(DateTime date);

	public void CreateList (List<CarSpeedData> dataList);
}