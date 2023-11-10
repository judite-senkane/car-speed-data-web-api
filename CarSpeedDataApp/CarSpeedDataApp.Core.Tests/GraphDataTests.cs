using CarSpeedDataApp.Core.Models;
using FluentAssertions;

namespace CarSpeedDataApp.Tests
{
	[TestClass]
	public class GraphDataTests
	{
		private const int SPEED = 70;
		private const int HOUR = 1;
		private GraphData _graphData;

		[TestMethod]
		public void GraphDataSetter_ObjectCreated()
		{
			_graphData = new GraphData()
			{
				Hour = HOUR,
				AverageSpeed = SPEED
			};

			_graphData.Should().BeOfType<GraphData>();
			_graphData.Should().NotBeNull();
		}

		[TestMethod]
		public void GraphDataGetter_CorrectDataReturned()
		{
			_graphData = new GraphData()
			{
				Hour = HOUR,
				AverageSpeed = SPEED
			};

			_graphData.Hour.Should().Be(HOUR);
			_graphData.AverageSpeed.Should().Be(SPEED);
		}
	}
}
