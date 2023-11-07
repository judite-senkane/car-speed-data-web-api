using CarSpeedDataApp.Core.Models;

namespace CarSpeedDataApp.Core.Services;

public interface ICarSpeedDataService
{
	public PagedCarSpeedData GetData(int? page, DateTime? dateFrom, DateTime? dateTo, int? speed);
	public List<double> GetDay(DateTime date);
	public void CreateList(List<CarSpeedData> dataList);
	public void ClearDatabase();
}