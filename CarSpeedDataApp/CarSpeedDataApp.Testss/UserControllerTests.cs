using CarSpeedDataApp.Controllers;
using CarSpeedDataApp.Core.Models;
using CarSpeedDataApp.Core.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq.AutoMock;

namespace CarSpeedDataApp.Tests
{
	[TestClass]
	public class UserControllerTests
	{
		private static readonly List<double> _dummyData = new() { 34.56, 45.32, 45.76, 76.54, 90.32, 43.69 };

		private static readonly CarSpeedData _carSpeedData = new CarSpeedData()
		{ DateAndTime = new DateTime(20, 08, 01, 00, 00, 01), Id = 1, LicenseNumber = "P9S56K", SpeedKmH = 70 };

		private static readonly PagedCarSpeedData PagedCarSpeedData = new PagedCarSpeedData()
		{ Items = new List<CarSpeedData>() { _carSpeedData }, TotalPages = 1 };
		private readonly DateTime _day = new DateTime(2020, 08, 01);
		private UserController _userController;
		private AutoMocker _mocker;

		[TestInitialize]
		public void Setup()
		{
			_mocker = new AutoMocker();
			var carSpeedDataServiceMock = _mocker.GetMock<ICarSpeedDataService>();
			_userController = new UserController(carSpeedDataServiceMock.Object);
		}

		[TestMethod]
		public void GetDayAverage_WithValidData_OkResponseReceived()
		{
			//Arrange
			var response = new AverageSpeedDataResponse() { AverageSpeedData = _dummyData };
			_mocker.GetMock<ICarSpeedDataService>().Setup(d => d.GetDay(_day)).Returns(_dummyData);

			//Act
			var result = _userController.GetDayAverage(_day);

			//Assert
			result.Should().BeOfType<OkObjectResult>();
			var okObjectResult = result.Should().BeOfType<OkObjectResult>().Subject;
			var responseData = okObjectResult.Value.Should().BeOfType<AverageSpeedDataResponse>();
		}

		[TestMethod]
		public void GetDayAverage_WithInvalidData_NotFoundResponseReceived()
		{
			//Arrange
			var day = new DateTime(2020, 08, 02);
			var response = new AverageSpeedDataResponse() { AverageSpeedData = _dummyData };
			_mocker.GetMock<ICarSpeedDataService>().Setup(d => d.GetDay(_day)).Returns(_dummyData);

			//Act
			var result = _userController.GetDayAverage(day);

			//Assert
			result.Should().BeOfType<NotFoundResult>();
		}

		[TestMethod]
		public void GetData_WithNoFilters_OkResponseReceived()
		{
			//Arrange
			var response = PagedCarSpeedData;
			_mocker.GetMock<ICarSpeedDataService>().Setup(d => d.GetData(null, null, null, null)).Returns(response);

			//Act
			var result = _userController.GetData(null, null, null, null);

			//Assert
			result.Should().BeOfType<OkObjectResult>();
			var okObjectResult = result.Should().BeOfType<OkObjectResult>().Subject;
			var responseData = okObjectResult.Value.Should().BeOfType<PagedCarSpeedData>();
		}

		[TestMethod]
		public void GetData_WithOneFilter_OkResponseReceived()
		{
			//Arrange
			var page = 1;
			var response = PagedCarSpeedData;
			_mocker.GetMock<ICarSpeedDataService>().Setup(d => d.GetData(page, null, null, null)).Returns(response);

			//Act
			var result = _userController.GetData(page, null, null, null);

			//Assert
			result.Should().BeOfType<OkObjectResult>();
			var okObjectResult = result.Should().BeOfType<OkObjectResult>().Subject;
			var responseData = okObjectResult.Value.Should().BeOfType<PagedCarSpeedData>();
		}

		[TestMethod]
		public void GetData_WithTwoFilters_OkResponseReceived()
		{
			//Arrange
			var page = 1;
			var dateFrom = new DateTime(2020, 08, 01);
			var response = PagedCarSpeedData;
			_mocker.GetMock<ICarSpeedDataService>().Setup(d => d.GetData(page, dateFrom, null, null)).Returns(response);

			//Act
			var result = _userController.GetData(page, dateFrom, null, null);

			//Assert
			result.Should().BeOfType<OkObjectResult>();
			var okObjectResult = result.Should().BeOfType<OkObjectResult>().Subject;
			var responseData = okObjectResult.Value.Should().BeOfType<PagedCarSpeedData>();
		}

		[TestMethod]
		public void GetData_WithThreeFilters_OkResponseReceived()
		{
			//Arrange
			var page = 1;
			var dateFrom = new DateTime(2020, 08, 01);
			var dateTo = dateFrom;
			var response = PagedCarSpeedData;
			_mocker.GetMock<ICarSpeedDataService>().Setup(d => d.GetData(1, dateFrom, dateTo, null)).Returns(response);

			//Act
			var result = _userController.GetData(page, dateFrom, dateTo, null);

			//Assert
			result.Should().BeOfType<OkObjectResult>();
			var okObjectResult = result.Should().BeOfType<OkObjectResult>().Subject;
			var responseData = okObjectResult.Value.Should().BeOfType<PagedCarSpeedData>();
		}

		[TestMethod]
		public void GetData_WithFourFilters_OkResponseReceived()
		{
			//Arrange
			var page = 1;
			var dateFrom = new DateTime(2020, 08, 01);
			var dateTo = dateFrom;
			var speed = 70;
			var response = PagedCarSpeedData;
			_mocker.GetMock<ICarSpeedDataService>().Setup(d => d.GetData(1, dateFrom, dateTo, speed)).Returns(response);

			//Act
			var result = _userController.GetData(page, dateFrom, dateTo, speed);

			//Assert
			result.Should().BeOfType<OkObjectResult>();
			var okObjectResult = result.Should().BeOfType<OkObjectResult>().Subject;
			var responseData = okObjectResult.Value.Should().BeOfType<PagedCarSpeedData>();
		}

		[TestMethod]
		public void GetData_WithInvalidData_NotFoundResultReturned()
		{
			//Arrange
			var page = 1;
			var dateFrom = new DateTime(2020, 08, 01);
			var dateTo = dateFrom;
			var speed = 70;
			var response = PagedCarSpeedData;
			_mocker.GetMock<ICarSpeedDataService>().Setup(d => d.GetData(1, dateFrom, dateTo, speed)).Returns(response);

			//Act
			var result = _userController.GetData(2, dateFrom, dateTo, speed);

			//Assert
			result.Should().BeOfType<NotFoundResult>();
		}
	}
}
