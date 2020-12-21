using System;

namespace LabTest2.Apps.Web.Shared.DTOs
{
	public class WeatherForecastDTO
	{
		private int _temperatureF;
		public DateTime Date { get; set; }

		public int TemperatureC { get; set; }

		public string Summary { get; set; } = string.Empty;

		public int TemperatureF
		{
			get => 32 + (int)(TemperatureC / 0.5556);
			set => _temperatureF =  32 + (int)(TemperatureC / 0.5556);
		}
	}
}
