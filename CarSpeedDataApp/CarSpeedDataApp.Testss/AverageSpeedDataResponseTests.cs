using CarSpeedDataApp.Models;
using FluentAssertions;

namespace CarSpeedDataApp.Tests
{
    [TestClass]
	public class AverageSpeedDataResponseTests
	{
		private static readonly List<double> _dummyData = new() { 34.56, 45.32, 45.76, 76.54, 90.32, 43.69 };
		private const double FIRST_RECORD = 34.56;
		private static readonly int _dataCount = _dummyData.Count;
		private static AverageSpeedDataResponse _response;

		[TestMethod]
		public void AverageSpeedDataResponseSetter_ObjectCreated()
		{
			AverageSpeedDataResponse response = new() { AverageSpeedData = _dummyData };
			response.AverageSpeedData.Count.Should().Be(_dataCount);
			response.AverageSpeedData[0].Should().Be(FIRST_RECORD);
		}

		[TestInitialize]
		public void SetUp()
		{
			_response = new() { AverageSpeedData = _dummyData };
		}

		[TestMethod]
		public void AverageSpeedDataResponseGetter_CorrectDataReturned()
		{
			var result = _response.AverageSpeedData;
			result.Count.Should().Be(_dataCount);
			result[0].Should().Be(FIRST_RECORD);
		}
	}
}