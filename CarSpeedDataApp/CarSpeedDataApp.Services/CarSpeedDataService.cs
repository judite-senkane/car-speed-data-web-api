using CarSpeedDataApp.Core.Models;
using CarSpeedDataApp.Core.Services;

namespace CarSpeedDataApp.Services;

public class CarSpeedDataService : ICarSpeedDataService<CarSpeedData>
{
	private ICarSpeedDataService<CarSpeedData> _context;

	public CarSpeedDataService(ICarSpeedDataService<CarSpeedData> context)
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
}