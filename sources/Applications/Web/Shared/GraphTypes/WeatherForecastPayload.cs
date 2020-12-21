using LabTest2.Apps.Web.Shared.DTOs;

using System.Collections.Generic;

namespace LabTest2.Apps.Web.Shared.GraphTypes
{
	public class WeatherForecastPayload
		: PayloadBase
	{
		public WeatherForecastDTO? Forecast { get; }

		public WeatherForecastPayload(
			WeatherForecastDTO forecast
		)
			=> Forecast = forecast;

		public WeatherForecastPayload(
			IReadOnlyList<UserError> errors
		)
		    : base(errors)
		{
		}
	}

	public class WeatherForecastRequestPayload
		: PayloadBase
	{
		public string Response { get; } = string.Empty;

		public WeatherForecastRequestPayload(
			string response
		)
			: base(null)
			=> Response = response;

		public WeatherForecastRequestPayload(
			IReadOnlyList<UserError> errors
		)
		    : base(errors)
		{
		}
	}
}
