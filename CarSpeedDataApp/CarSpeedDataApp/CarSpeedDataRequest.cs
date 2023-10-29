﻿using System.ComponentModel.DataAnnotations;

namespace CarSpeedDataApp
{
	public class CarSpeedDataRequest
	{
		public DateTime DateAndTime { get; set; }

		public int SpeedKmH { get; set; }

		public string LicenseNumber { get; set; }
	}
}