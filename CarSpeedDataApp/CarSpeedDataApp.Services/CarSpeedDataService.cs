using CarSpeedDataApp.Core.Models;
using CarSpeedDataApp.Core.Services;
using CarSpeedDataApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CarSpeedDataApp.Services;

public class CarSpeedDataService : ICarSpeedDataService
{
	private readonly ICarSpeedDataDbContext _context;
	private const int pagePerView = 20;

	public CarSpeedDataService(ICarSpeedDataDbContext context)
	{
		_context = context;
	}

	public async Task<PagedCarSpeedData> GetData(int? page, DateTime? dateFrom, DateTime? dateTo, int? speed)
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

		var items = await dataQuery.Skip((page.Value - 1) * pagePerView).Take(pagePerView).ToListAsync();

		var totalItemCount = await dataQuery.CountAsync();
		var totalPages = (int)Math.Ceiling(totalItemCount / (double)pagePerView);

		return new PagedCarSpeedData
		{
			Items = items,
			TotalPages = totalPages
		};
	}

	public async Task<List<GraphData>> GetDay(DateTime date)
	{
		var result = new List<GraphData>();
		var filteredData = await _context.CarSpeedData.Where(d => d.DateAndTime.Date == date.Date).ToListAsync();

		var groupedResult = filteredData.GroupBy(d => d.DateAndTime.Hour).ToList();

		foreach(var group in groupedResult)
		{
			result.Add(new GraphData
			{
				Hour = group.Key,
				AverageSpeed = group.Select(e => e.SpeedKmH).Average()
			}); ;
		}

		return result;
	}
	public async Task CreateList(List<CarSpeedData> dataList)
	{
		await _context.CarSpeedData.AddRangeAsync(dataList);
		_context.SaveChanges();
	}

	public void ClearDatabase()
	{
		 _context.CarSpeedData.RemoveRange(_context.CarSpeedData);
		 _context.SaveChanges();
	}
}