using CarSpeedDataApp.Core.Models;
using FluentAssertions;

namespace CarSpeedDataApp.Core.Tests
{
	public class PagedCarSpeedDataTests
	{
		private const int ID = 1;
		private static readonly DateTime _dateAndTime = new DateTime(2020, 08, 01, 05, 31, 00);
		private const int SPEED = 70;
		private const string LICENSE_NUMBER = "PS86FK";
		private PagedCarSpeedData _pagedCarSpeedData;
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
			_pagedCarSpeedData = new PagedCarSpeedData()
			{
				Items = _carSpeedData,
				TotalPages = TOTAL_PAGES
			};

			_pagedCarSpeedData.Should().BeOfType<PagedCarSpeedData>();
			_pagedCarSpeedData.Should().NotBeNull();
		}

		[TestMethod]
		public void CarSpeedDataGetter_CorrectDataReturned()
		{
			_pagedCarSpeedData = new PagedCarSpeedData()
			{
				Items = _carSpeedData,
				TotalPages = TOTAL_PAGES
			};

			_pagedCarSpeedData.Items.Should().BeOfType<CarSpeedData>();
			_pagedCarSpeedData.Items.Should().HaveCount(_carSpeedData.Count);
			_pagedCarSpeedData.TotalPages.Should().Be(TOTAL_PAGES);
		}
	}
}
