using System.ComponentModel.DataAnnotations;

namespace CarSpeedDataApp.Core.Models

{
	public class CarSpeedData
	{
		public int Id { get; set; }

		public DateTime DateAndTime { get; set; }

		public int SpeedKmH { get; set; }

		[StringLength(50)]
		public string LicenseNumber { get; set; }
	}
}