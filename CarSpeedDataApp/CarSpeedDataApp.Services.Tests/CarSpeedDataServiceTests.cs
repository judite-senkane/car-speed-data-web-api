using CarSpeedDataApp.Core.Models;
using CarSpeedDataApp.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CarSpeedDataApp.Services.Tests
{
	[TestClass]
	public class CarSpeedDataServiceTests
	{
		private TestCarSpeedDataDbContext _carSpeedDataContext;
		private CarSpeedDataService _carSpeedDataService;
		private static readonly List<CarSpeedData> _basicData = new()
		{
			new CarSpeedData()
			{
				Id = 1,
				DateAndTime = new DateTime(2020, 08, 01, 00, 04, 00),
				SpeedKmH = 60,
				LicenseNumber = "G9VM45"
			},
			new CarSpeedData()
			{
				Id = 2,
				DateAndTime = new DateTime(2021, 05, 04, 16, 17, 00),
				SpeedKmH = 70,
				LicenseNumber = "SP76CR"
			}
		};

		[TestMethod]
		public void GetData_WithNoFilters_DataResultReturned()
		{
			//Arrange
			CreateInstanceOfDatabase();
			AddData();

			//Act
			var result = _carSpeedDataService.GetData(null, null, null, null);

			//Assert
			result.Should().BeOfType<DataResult>();
			result.Items.Should().HaveCount(2);
			result.TotalPages.Should().Be(1);

			//Cleanup
			ClearDatabase();
		}

		[TestMethod]
		public void GetData_WithTwoFilters_DataResultReturned()
		{
			//Arrange
			CreateInstanceOfDatabase();
			AddData();

			var searchDateFrom = new DateTime(2021,01,01);
			var speed = 50;

			//Act
			var result = _carSpeedDataService.GetData(null, searchDateFrom, null, speed);

			//Assert
			result.Should().BeOfType<DataResult>();
			result.Items.Should().HaveCount(1);
			result.TotalPages.Should().Be(1);

			//Cleanup
			ClearDatabase();
		}

		[TestMethod]
		public void GetDay_ListDoubleReceived()
		{
			//Arrange
			CreateInstanceOfDatabase();
			AddData();

			var expectedResult = new List<double> { 60, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
			var day = new DateTime(2020, 08, 01);

			//Act
			var result = _carSpeedDataService.GetDay(day);

			//Assert
			result.Should().NotBeNull();
			result.Should().HaveCount(24);
			result.Should().Equal(expectedResult);

			//Cleanup
			ClearDatabase();
		}

		[TestMethod]
		public void CreateList_WithValidData_DataAddedToDatabase()
		{
			//Arrange
			CreateInstanceOfDatabase();

			//Act
			_carSpeedDataService.CreateList(_basicData);

			//Assert
			_carSpeedDataContext.CarSpeedData.Should().Equal(_basicData);

			//Cleanup
			ClearDatabase();
		}

		[TestMethod]
		public void CreateList_WithNoData_NoDataAddedToDatabase()
		{
			//Arrange
			CreateInstanceOfDatabase();
			//Act

			_carSpeedDataService.CreateList(new List<CarSpeedData>());

			//Assert
			_carSpeedDataContext.CarSpeedData.Should().BeEmpty();

			//Cleanup
			ClearDatabase();
		}

		private void CreateInstanceOfDatabase()
		{
			var options = new DbContextOptionsBuilder<CarSpeedDataDbContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
				.Options;

			_carSpeedDataContext = new TestCarSpeedDataDbContext(options);
			_carSpeedDataService = new CarSpeedDataService(_carSpeedDataContext);
		}

		private void ClearDatabase()
		{
			_carSpeedDataContext.RemoveRange(_carSpeedDataContext.CarSpeedData);
			_carSpeedDataContext.SaveChanges();
		}

		private void AddData()
		{
			_carSpeedDataContext.CarSpeedData.AddRange(_basicData);
			_carSpeedDataContext.SaveChanges();
		}
	}
}