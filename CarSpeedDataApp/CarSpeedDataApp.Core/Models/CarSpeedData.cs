using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarSpeedDataApp.Core.Models

{
	public class CarSpeedData
	{
		public int Id { get; set; }

		[StringLength(100)]
		public DateTime DateAndTime { get; set; }

		public int SpeedKmH { get; set; }

		[StringLength(50)]
		public string LicenseNumber { get; set; }
	}
}