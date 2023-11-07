using CarSpeedDataApp.Core.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq.AutoMock;
using System.Reflection;

namespace CarSpeedDataApp.Services.Tests
{
	[TestClass]
	public class FileProcessingServiceTests
	{
		private FileProcessingService _fileProcessingService;
		private AutoMocker _mocker;

		private string _validFilePath;
		private string _invalidFilePath;

		[TestInitialize]
		public void Setup()
		{
			_fileProcessingService = new FileProcessingService();
			_mocker = new AutoMocker();

			_invalidFilePath = Path.Combine(GetAssemblyDirectory(), "TestFiles", "InvalidFile.txt");
		}

		[TestMethod]
		public async Task ExtractDataFromFile_WithValidFile_ListWithCorrectItemsReturned()
		{
			//Arrange
			var formFileMock = _mocker.GetMock<IFormFile>();
			formFileMock.Setup(f => f.FileName).Returns("ValidFile.txt");
			_validFilePath = Path.Combine(GetAssemblyDirectory(), "TestFiles", "ValidFile.txt");

			var fileBytes = File.ReadAllBytes(_validFilePath);
			var stream = new MemoryStream(fileBytes);
			formFileMock.Setup(f => f.OpenReadStream()).Returns(stream);

			//Act
			var result = await _fileProcessingService.ExtractDataFromFile(formFileMock.Object);

			//Assert
			result.Should().HaveCount(2);
			result.Should().BeOfType<List<CarSpeedData>>();
		}

		[TestMethod]
		public void ExtractDataFromFile_WithInvalidFile_ThrowsFormatException()
		{
			//Arrange
			var formFileMock = _mocker.GetMock<IFormFile>();
			formFileMock.Setup(f => f.FileName).Returns("InvalidFile.txt");
			_invalidFilePath = Path.Combine(GetAssemblyDirectory(), "TestFiles", "InvalidFile.txt");

			var fileBytes = File.ReadAllBytes(_invalidFilePath);
			var stream = new MemoryStream(fileBytes);
			formFileMock.Setup(f => f.OpenReadStream()).Returns(stream);

			//Act
			var action = async () => _fileProcessingService.ExtractDataFromFile(formFileMock.Object);

			//Assert
			action.Should().ThrowAsync<FormatException>();
		}

		private string GetAssemblyDirectory()
		{
			return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		}
	}
}
