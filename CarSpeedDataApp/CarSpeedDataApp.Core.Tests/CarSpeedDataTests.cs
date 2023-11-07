using CarSpeedDataApp.Core.Models;
using FluentAssertions;

namespace CarSpeedDataApp.Core.Tests
{
	[TestClass]
	public class CarSpeedDataTests
	{
		private CarSpeedData _carSpeedData;
		private const int ID = 1;
		private readonly DateTime _dateAndTime = new DateTime(2020, 08, 01, 05, 31, 00);
		private const int SPEED = 70;
		private const string LICENSE_NUMBER = "PS86FK";

		[TestMethod]
		public void CarSpeedDataSetter_CorrectDataInserted()
		{
			_carSpeedData = new CarSpeedData()
			{
				Id = ID,
				DateAndTime = _dateAndTime,
				SpeedKmH = SPEED,
				LicenseNumber = LICENSE_NUMBER
			};

			_carSpeedData.Should().BeOfType<CarSpeedData>();
			_carSpeedData.Should().NotBeNull();
		}

		[TestMethod]
		public void CarSpeedDataGetter_CorrectDataReturned()
		{
			_carSpeedData = new CarSpeedData()
			{
				Id = ID,
				DateAndTime = _dateAndTime,
				SpeedKmH = SPEED,
				LicenseNumber = LICENSE_NUMBER
			};

			_carSpeedData.Id.Should().Be(ID);
			_carSpeedData.DateAndTime.Should().Be(_dateAndTime);
			_carSpeedData.SpeedKmH.Should().Be(SPEED);
			_carSpeedData.LicenseNumber.Should().Be(LICENSE_NUMBER);
		}
	}
}