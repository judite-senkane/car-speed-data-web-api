using CarSpeedDataApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CarSpeedDataApp.Data;

public interface ICarSpeedDataDbContext
{
	DbSet<CarSpeedData> CarSpeedData { get; set; }
	int SaveChanges();
}