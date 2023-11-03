using System.Text.Json.Serialization;

namespace CarSpeedDataApp
{
	public class AverageSpeedDataResponse
	{
		[JsonPropertyName("speed")]
		public List<double> AverageSpeedData { get; set; }
	}
}
