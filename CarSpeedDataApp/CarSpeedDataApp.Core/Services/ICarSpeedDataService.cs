using CarSpeedDataApp.Core.Models;

namespace CarSpeedDataApp.Core.Services;

public interface ICarSpeedDataService
{
	public Task<PagedCarSpeedData> GetData(int? page, DateTime? dateFrom, DateTime? dateTo, int? speed);
	public Task<List<GraphData>> GetDay(DateTime date);
	public Task CreateList(List<CarSpeedData> dataList);
	public Task ClearDatabase();
}