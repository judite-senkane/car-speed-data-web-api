using CarSpeedDataApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CarSpeedDataApp.Data;

public interface ICarSpeedDataDbContext
{
	DbSet<CarSpeedData> CarSpeedData { get; set; }
	int SaveChanges();
	void AddRange(params object[] entities);
	void RemoveRange(params object[] entities);
	DbSet<T> Set<T>() where T: class;
}