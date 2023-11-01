using CarSpeedDataApp.Core.Models;

namespace CarSpeedDataApp.Core.Services;

public interface ICarSpeedDataService
{
	public DataResult GetData(int page, DateTime? dateFrom, DateTime? dateTo, int? speed);

	public IEnumerable<double> GetDay(DateTime date);

	public void CreateList (List<CarSpeedData> dataList);
}