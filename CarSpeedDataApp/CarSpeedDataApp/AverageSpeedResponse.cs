using System.Text.Json.Serialization;

namespace CarSpeedDataApp
{
	public class AverageSpeedResponse
	{
		[JsonPropertyName("speed")]
		public List<double> AverageSpeedData { get; set; }
	}
}
