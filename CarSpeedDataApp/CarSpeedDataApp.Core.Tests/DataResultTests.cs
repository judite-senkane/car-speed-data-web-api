using CarSpeedDataApp.Core.Models;
using FluentAssertions;

namespace CarSpeedDataApp.Core.Tests
{
	public class DataResultTests
	{
		private const int ID = 1;
		private static readonly DateTime _dateAndTime = new DateTime(2020, 08, 01, 05, 31, 00);
		private const int SPEED = 70;
		private const string LICENSE_NUMBER = "PS86FK";
		private DataResult _dataResult;
		private const int TOTAL_PAGES = 1;

		private static readonly CarSpeedData _oneRecord = new CarSpeedData()
		{
			Id = ID,
			DateAndTime = _dateAndTime,
			SpeedKmH = SPEED,
			LicenseNumber = LICENSE_NUMBER
		};

		private readonly List<CarSpeedData> _carSpeedData = new List<CarSpeedData>() { _oneRecord };

		[TestMethod]
		public void DataResultSetter_ObjectCreated()
		{
			_dataResult = new DataResult()
			{
				Items = _carSpeedData,
				TotalPages = TOTAL_PAGES
			};

			_dataResult.Should().BeOfType<DataResult>();
			_dataResult.Should().NotBeNull();
		}

		[TestMethod]
		public void CarSpeedDataGetter_CorrectDataReturned()
		{
			_dataResult = new DataResult()
			{
				Items = _carSpeedData,
				TotalPages = TOTAL_PAGES
			};

			_dataResult.Items.Should<CarSpeedData>();
			_dataResult.Items.Should().HaveCount(_carSpeedData.Count);
			_dataResult.TotalPages.Should().Be(TOTAL_PAGES);
		}
	}
}
