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
		private const string TEST_CONTENT = "2020-08-01 00:04:35\t70\tSP8224\n2020-08-01 00:07:55\t69\tMS6174";
		private const string INCORRECT_FORMAT_DATA = "70\tSP8224\t2020-08-01 00:04:35";
		private FileUploadController _fileUploadController;
		private AutoMocker _mocker;
		private Mock<ICarSpeedDataService> CarSpeedDataServiceMock;

		[TestInitialize]
		public void SetUp()
		{
			_mocker = new AutoMocker();
			CarSpeedDataServiceMock = _mocker.GetMock<ICarSpeedDataService>();
			_fileUploadController = new FileUploadController(CarSpeedDataServiceMock.Object);
		}

		[TestMethod]
		public async Task UploadFile_Success()
		{
			// Arrange
			var formFileMock = _mocker.GetMock<IFormFile>();
			formFileMock.Setup(f => f.FileName).Returns("test.txt");
			formFileMock.Setup(f => f.OpenReadStream())
				.Returns(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(TEST_CONTENT)));


			// Act
			var result = await _fileUploadController.UploadFile(formFileMock.Object);

			// Assert
			result.Should().BeOfType<CreatedResult>();
			CarSpeedDataServiceMock.Verify(s => s.CreateList(It.IsAny<List<CarSpeedData>>()), Times.Once);
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
		}

		[TestMethod]
		public async Task UploadFile_WithIncorrectDataFormat_BadRequestReturned()
		{
			// Arrange
			var formFileMock = _mocker.GetMock<IFormFile>();
			formFileMock.Setup(f => f.FileName).Returns("test.txt");
			formFileMock.Setup(f => f.OpenReadStream())
				.Returns(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(INCORRECT_FORMAT_DATA)));


			// Act
			var result = await _fileUploadController.UploadFile(formFileMock.Object);

			// Assert
			result.Should().BeOfType<BadRequestObjectResult>();
			var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
			badRequestResult.Value.Should().BeOfType<string>();
			badRequestResult.Value.Should().Be("File data is not in the correct format");
			CarSpeedDataServiceMock.Verify(s => s.CreateList(It.IsAny<List<CarSpeedData>>()), Times.Never);
		}

		[TestMethod]
		public async Task UploadFile_WithEmptyFields_BadRequestReturned()
		{
			// Arrange
			var formFileMock = _mocker.GetMock<IFormFile>();
			formFileMock.Setup(f => f.FileName).Returns("test.txt");
			formFileMock.Setup(f => f.OpenReadStream())
				.Returns(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(TEST_CONTENT + "" + "\n\t\t\tMQ7148")));

			// Act
			var result = await _fileUploadController.UploadFile(formFileMock.Object);

			// Assert
			result.Should().BeOfType<BadRequestObjectResult>();
			var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
			badRequestResult.Value.Should().BeOfType<string>();
			badRequestResult.Value.Should().Be("File data is not in the correct format");
			CarSpeedDataServiceMock.Verify(s => s.CreateList(It.IsAny<List<CarSpeedData>>()), Times.Never);
		}
	}
}
