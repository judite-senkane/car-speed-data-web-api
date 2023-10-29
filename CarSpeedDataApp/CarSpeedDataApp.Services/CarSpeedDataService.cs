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

	public IEnumerable<CarSpeedData> GetDay(DateTime date)
	{
		throw new NotImplementedException();
	}

	public void Create(List<CarSpeedData> data)
	{
		throw new NotImplementedException();
	}

	public void Create(CarSpeedData data)
	{
		{
			_context.CarSpeedData.Add(data);
			_context.SaveChanges();
		}
	}
}