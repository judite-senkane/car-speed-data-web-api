using CarSpeedDataApp.Controllers;
using CarSpeedDataApp.Core.Models;
using CarSpeedDataApp.Core.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;

namespace CarSpeedDataApp.Tests
{
	[TestClass]
	public class FileUploadControllerTests
	{
		private FileUploadController _fileUploadController;
		private AutoMocker _mocker;
		private Mock<ICarSpeedDataService> _carSpeedDataServiceMock;
		private Mock<IFileProcessingService> _fileProcessingServiceMock;

		[TestInitialize]
		public void SetUp()
		{
			_mocker = new AutoMocker();
			_carSpeedDataServiceMock = _mocker.GetMock<ICarSpeedDataService>();
			_fileProcessingServiceMock = _mocker.GetMock<IFileProcessingService>();
			_fileUploadController = new FileUploadController(_carSpeedDataServiceMock.Object, _fileProcessingServiceMock.Object);
		}

		[TestMethod]
		public async Task UploadFile_WithValidData_CreatedResultReturned()
		{
			// Arrange
			var formFileMock = _mocker.GetMock<IFormFile>();

			var dataList = new List<CarSpeedData>()
			{
				new CarSpeedData()
				{
					DateAndTime = new DateTime(2020, 08, 01, 00, 04, 35),
					SpeedKmH = 70,
					LicenseNumber = "SP8224"
				},
				new CarSpeedData()
				{
					DateAndTime = new DateTime(2020, 08, 01, 00, 07, 55),
					SpeedKmH = 69,
					LicenseNumber = "MS6174"
				}
			};

			formFileMock.Setup(f => f.FileName).Returns("test.txt");
			_fileProcessingServiceMock.Setup(p => p.ExtractDataFromFile(It.IsAny<IFormFile>())).ReturnsAsync(dataList);

			// Act
			var result = await _fileUploadController.UploadFile(formFileMock.Object);

			// Assert
			result.Should().BeOfType<CreatedResult>();
			_fileProcessingServiceMock.Verify(f => f.ExtractDataFromFile(It.IsAny<IFormFile>()), Times.Once);
			_carSpeedDataServiceMock.Verify(s => s.CreateList(It.IsAny<List<CarSpeedData>>()), Times.Once);
		}

		[TestMethod]
		public async Task UploadFile_WithNullFile_BadRequestReturned()
		{
			// Act
			var result = await _fileUploadController.UploadFile(null);

			// Assert
			result.Should().BeOfType<BadRequestObjectResult>();
			var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
			badRequestResult.Value.Should().BeOfType<string>();
			badRequestResult.Value.Should().Be("Invalid file upload");
			_fileProcessingServiceMock.Verify(f => f.ExtractDataFromFile(It.IsAny<IFormFile>()), Times.Never);
			_carSpeedDataServiceMock.Verify(s => s.CreateList(It.IsAny<List<CarSpeedData>>()), Times.Never);

		}

		[TestMethod]
		public async Task UploadFile_WithIncorrectDataFormat_BadRequestReturned()
		{
			// Arrange
			var formFileMock = _mocker.GetMock<IFormFile>();
			formFileMock.Setup(f => f.FileName).Returns("test.txt");
			_fileProcessingServiceMock.Setup(p => p.ExtractDataFromFile(It.IsAny<IFormFile>()))
				.Throws<FormatException>();

			// Act
			var result = await _fileUploadController.UploadFile(formFileMock.Object);

			// Assert
			result.Should().BeOfType<BadRequestObjectResult>();
			var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
			badRequestResult.Value.Should().BeOfType<string>();
			badRequestResult.Value.Should().Be("File data is not in the correct format");
			_fileProcessingServiceMock.Verify(f => f.ExtractDataFromFile(It.IsAny<IFormFile>()), Times.Once);
			_carSpeedDataServiceMock.Verify(s => s.CreateList(It.IsAny<List<CarSpeedData>>()), Times.Never);
		}
	}
}
