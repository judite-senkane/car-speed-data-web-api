using CarSpeedDataApp.Core.Models;
using CarSpeedDataApp.Core.Services;
using CarSpeedDataApp.Data;

namespace CarSpeedDataApp.Services;

public class CarSpeedDataService : ICarSpeedDataService
{
	private readonly CarSpeedDataDbContext _context;

	public CarSpeedDataService(CarSpeedDataDbContext context)
	{
		_context = context;
	}
	public IEnumerable<CarSpeedData> GetRange(DateTime? dateFrom, DateTime? dateTo, int? speed)
	{
		throw new NotImplementedException();
	}

	public IEnumerable<int> GetDay(DateTime date)
	{
		throw new NotImplementedException();
	}

	public void CreateList(List<CarSpeedData> dataList)
	{
		foreach (var item in dataList)
		{
			_context.Add(item);
		}
	}

	public void SaveChanges()
	{
		_context.SaveChanges();
	}
}