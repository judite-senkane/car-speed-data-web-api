using CarSpeedDataApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CarSpeedDataApp.Services.Tests
{
	public class TestCarSpeedDataDbContext : CarSpeedDataDbContext
	{
		public TestCarSpeedDataDbContext(DbContextOptions<CarSpeedDataDbContext> options) : base(options)
		{
		}
	}
}
