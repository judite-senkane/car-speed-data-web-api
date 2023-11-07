using CarSpeedDataApp.Core.Models;
using CarSpeedDataApp.Core.Services;
using CarSpeedDataApp.Data;

namespace CarSpeedDataApp.Services;

public class CarSpeedDataService : ICarSpeedDataService
{
	private readonly ICarSpeedDataDbContext _context;
	private const int pagePerView = 20;

	public CarSpeedDataService(ICarSpeedDataDbContext context)
	{
		_context = context;
	}

	public PagedCarSpeedData GetData(int? page, DateTime? dateFrom, DateTime? dateTo, int? speed)
	{
		if (page < 1 || page == null) page = 1;

		var dataQuery = _context.CarSpeedData.AsQueryable();

		if (dateFrom != null)
		{
			dataQuery = dataQuery.Where(f => f.DateAndTime.Date >= dateFrom.Value.Date);
		}

		if (dateTo != null)
		{
			dataQuery = dataQuery.Where(t => t.DateAndTime.Date <= dateTo.Value.Date);
		}

		if (speed != null)
		{
			dataQuery = dataQuery.Where(s => s.SpeedKmH >= speed.Value);
		}

		var items = dataQuery.Skip((page.Value - 1) * pagePerView).Take(pagePerView).ToList();
		var totalPages = (int)Math.Ceiling(dataQuery.Count() / (double)pagePerView);

		return new PagedCarSpeedData
		{
			Items = items,
			TotalPages = totalPages
		};
	}

	public List<double> GetDay(DateTime date)
	{
		var filteredData = _context.CarSpeedData.Where(d => d.DateAndTime.Date == date.Date);
		List<double> result = new List<double>();

		for (int i = 0; i < 24; i++)
		{
			var speedsForHour = filteredData.Where(d => (int)d.DateAndTime.Hour == i).Select(s => s.SpeedKmH).ToList();

			if (speedsForHour.Any())
			{
				var averageSpeed = speedsForHour.Average();
				result.Add(averageSpeed);
			}
			else
			{
				result.Add(0);
			}
		}

		return result;
	}
	public void CreateList(List<CarSpeedData> dataList)
	{
		_context.CarSpeedData.AddRange(dataList);
		_context.SaveChanges();
	}

	public void ClearDatabase()
	{
		_context.CarSpeedData.RemoveRange(_context.CarSpeedData);
		_context.SaveChanges();
	}
}