using HotChocolate.Types;

using LabTest2.Apps.Web.Shared.DTOs;

using System;

namespace LabTest2.Apps.Web.Shared.GraphTypes
{
	public class WeatherForecastInputType
		: InputObjectType<WeatherForecastDTO>
	{
		protected override void Configure(IInputObjectTypeDescriptor<WeatherForecastDTO> descriptor)
		{
			_ = descriptor
				.Field(f => f.Date)
				.Type<DateTimeType>()
				.DefaultValue(DateTime.Now)
				.Name(nameof(WeatherForecastDTO.Date));

			_ = descriptor
				.Field(f => f.Summary)
				.Type<StringType>()
				.Name(nameof(WeatherForecastDTO.Summary));

			_ = descriptor
				.Field(f => f.TemperatureC)
				.Type<IntType>()
				.Name(nameof(WeatherForecastDTO.TemperatureC));

			_ = descriptor
				.Field(f => f.TemperatureF)
				.Type<IntType>()
				.Ignore()
				.Name(nameof(WeatherForecastDTO.TemperatureF));
		}
	}
}
