using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using CarSpeedDataApp.Core.Models;
using CarSpeedDataApp.Core.Services;
using CarSpeedDataApp.Data;
using Microsoft.EntityFrameworkCore;

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
		var dataQuery = _context.CarSpeedData.AsQueryable();

		if (dateFrom != null)
		{
			dataQuery = dataQuery.Where(f => f.DateAndTime.Date <= dateFrom.Value.Date);
		}

		if (dateTo != null)
		{
			dataQuery = dataQuery.Where(t => t.DateAndTime.Date >= dateTo.Value.Date);
		}

		if (speed != null)
		{
			dataQuery = dataQuery.Where(s => s.SpeedKmH >= speed.Value);
		}

		return dataQuery.ToList();
	}

	public IEnumerable<double> GetDay(DateTime date)
	{
		var filteredData = _context.CarSpeedData.Where(d => d.DateAndTime.Date == date.Date);
		List<double> result = new List<double>();

		for (int i = 0; i < 24; i++)
		{
			var averageSpeed = filteredData.Where(d => d.DateAndTime.Hour == i).Select(s => s.SpeedKmH).Average();
			result.Add(averageSpeed);
		}

		return result;
	}

	public void CreateList(List<CarSpeedData> dataList)
	{
		_context.CarSpeedData.AddRange(dataList);
		_context.SaveChanges();
	}
}