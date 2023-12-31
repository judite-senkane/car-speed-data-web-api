using CarSpeedDataApp.Core.Services;
using CarSpeedDataApp.Data;
using CarSpeedDataApp.Services;
using Microsoft.EntityFrameworkCore;

namespace CarSpeedDataApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<CarSpeedDataDbContext>(options =>
				options.UseSqlServer(builder.Configuration
					.GetConnectionString("car-speed-data")));
			builder.Services.AddTransient<ICarSpeedDataService, CarSpeedDataService>();
			builder.Services.AddTransient<ICarSpeedDataDbContext, CarSpeedDataDbContext>();
			builder.Services.AddTransient<IFileProcessingService, FileProcessingService>();

			var app = builder.Build();

			app.UseCors(policyBuilder =>
			{
				policyBuilder
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
			});

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}