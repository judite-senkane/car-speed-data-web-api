using CarSpeedDataApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CarSpeedDataApp.Data;

public class CarSpeedDataDbContext : DbContext
{
	public CarSpeedDataDbContext(DbContextOptions<CarSpeedDataDbContext> options) : base(options)
	{

	}
	public DbSet<CarSpeedData> CarSpeedData { get; set; }
}